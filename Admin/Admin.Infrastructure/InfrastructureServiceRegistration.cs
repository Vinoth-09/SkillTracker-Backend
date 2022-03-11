using Admin.Application.Contracts;
using Admin.Domain.Entities;
using Admin.Infrastructure.Cache;
using Admin.Infrastructure.ESCache;
using Admin.Infrastructure.Repositories;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Admin.Infrastructure
{
    public static class InfrastructureServiceRegistration
    { 
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string endpointUri = configuration["DBData:EndpointUri"];
            string primaryKey = configuration["DBData:PrimaryPass"];
            string connectionString = configuration["DBData:ConnectionString"];
            string databaseId = configuration["DBData:DatabaseId"];
            string containerId = configuration["DBData:ContainerId"];

            services.AddEnyimMemcached(configuration);
            
            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey);
            services.AddSingleton<CosmosClient>(cosmosClient);
            Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).Result;
            database.CreateContainerIfNotExistsAsync(containerId, "/empId");
            services.AddDbContext<CosmosDbContext>(option => option.UseCosmos(connectionString, databaseId));

            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            services.AddScoped<IProfileRepository, ProfileRepository>();
            //services.AddSingleton<IPersonalInfoProvider, PersonalInfoProvider>();
            //services.AddSingleton<ISkillProvider, SkillProvider>();
            
            // services.AddSingleton<IElasticsearchRepository, ElasticsearchRepository>();
            
            return services;
        }

        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))

                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<ESDocument>(m => m
                    .PropertyName(c => c.EmpId, "empId")
                    .PropertyName(c => c.Name, "name")
                    .PropertyName(c => c.Skills, "skills")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
