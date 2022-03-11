using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Profile.Application.Contracts;
using Profile.Infrastructure.Repositories;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon;
using System;
using Microsoft.Azure.Cosmos;
using Profile.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Profile.Infrastructure
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

            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey);
            services.AddSingleton<CosmosClient>(cosmosClient);
            Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).Result;
            database.CreateContainerIfNotExistsAsync(containerId, "/empId");
            services.AddDbContext<SkillTrackerContext>(option => option.UseCosmos(connectionString, databaseId));
            
            services.AddScoped<IProfileRepository, ProfileRepository>();         
            return services;
        }
    }
}
