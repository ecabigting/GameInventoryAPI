using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using GameInventoryAPI.Repositories;
using GameInventoryAPI.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace GameInventoryAPI
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // returns the correct readable value
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // returns the correct readable value
            var settings = Configuration.GetSection(nameof(DBSettings)).Get<DBSettings>(); // setting up the connection string
            // inject mongoclient to app
            services.AddSingleton<IMongoClient>(serviceProvider => {                
                return new MongoClient(settings.ConnString);
            });

            services.AddSingleton<IItemsRepository, MongoDBItemsRepo>();
            
            services.AddControllers(options => {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameInventoryAPI", Version = "v1" });
            });
            
            // mongodb health check
            services.AddHealthChecks()
                    .AddMongoDb(
                        settings.ConnString, 
                        name:"dbstatus",
                        timeout: TimeSpan.FromSeconds(3),
                        tags: new []{"ready"});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameInventoryAPI v1"));
            }

            if(env.IsDevelopment()) {
                app.UseHttpsRedirection();
            }
            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/apistatus/ready", new HealthCheckOptions {
                    Predicate = (check) => check.Tags.Contains("ready"), // returns ready when database is ready, check line with comment `mongodb health check` under ConfigureServices
                    ResponseWriter = async(context,report) => 
                    {
                        var result = JsonSerializer.Serialize(
                            new {
                                status = report.Status.ToString(),
                                checks = report.Entries.Select(e => new {
                                    name = e.Key,
                                    status = e.Value.Status.ToString(),
                                    ex = e.Value.Exception != null ? e.Value.Exception.Message : "none",
                                    duration = e.Value.Duration.ToString()
                                })
                            }
                        );

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

                endpoints.MapHealthChecks("/apistatus/live", new HealthCheckOptions {
                    Predicate = (_) => false // return 200 that API is ready to serve request
                });
            });
        }
    }
}
