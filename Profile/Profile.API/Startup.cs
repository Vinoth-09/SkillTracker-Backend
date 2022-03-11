
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Profile.Application;
using Profile.Application.Exceptions;
using Profile.Infrastructure;
using System;
using System.IO;
using System.Net;

namespace Profile.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddApplicationServices();
                services.AddInfrastructureServices(Configuration);

                //// MassTransit-RabbitMQ Configuration
                //services.AddMassTransit(config =>
                //{
                //    config.UsingRabbitMq((ctx, cfg) =>
                //    {
                //        cfg.Host(Configuration["EventBusSettings:HostAddress"], c =>
                //        {
                //            c.Username(Configuration["EventBusSettings:username"]);
                //            c.Password(Configuration["EventBusSettings:password"]);
                //        });
                //        cfg.UseHealthCheck(ctx);
                //    });
                //});
                //services.AddMassTransitHostedService();
            }
            catch(Exception)
            {
                
            }

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            //The following code will allows us to use swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillTracker.API", Version = "v1" });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillTracker.API v1"));
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync("File error thrown!");
                        }
                        else if (exceptionHandlerPathFeature?.Error is ValidationException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var exception = exceptionHandlerPathFeature.Error as ValidationException;
                            string result = "{errors:" + JsonConvert.SerializeObject(exception.Errors) + "}";
                            await context.Response.WriteAsync(result);
                        }
                        else
                        {
                            await context.Response.WriteAsync("Internal Server error!");
                        }
                    });
                });
                // app.UseHsts();
            }
            app.UseCors("CorsAllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
