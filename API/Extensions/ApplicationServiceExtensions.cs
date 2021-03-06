using API.Data;
using API.DTOs.Pagination;
using API.DTOs.Validators;
using API.Helpers;
using API.Interfaces;
using API.Middleware;
using API.QuartzCore;
using API.Repositories;
using API.SignalR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<OnlineTracker>();
            services.AddScoped<IExternalClientContext, ExternalClientContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHubRepository, HubRepository>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                     connStr = config.GetConnectionString("DefaultConnection");
                    //connStr = config.GetConnectionString("SqLiteConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                 options.UseNpgsql(connStr);
                //options.UseSqlite(connStr).EnableSensitiveDataLogging();

            });
            services.AddScoped<IValidator<CityQuery>, CityQueryValidator>();
            services.AddScoped<IValidator<SensorsDataQuery>, SensorsDataQueryValidator>();
            services.AddHttpClient("gios", x =>
            {
                x.BaseAddress = new Uri("http://api.gios.gov.pl/pjp-api/rest/");
            });

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<RemidersJob>();
            services.AddSingleton(new Job(
                type: typeof(RemidersJob),
                expression: "0 5,15,30,45 * ? * *")); //every hour at minutes 5,15,30 and 45 
            

            return services;
        }
    }
}
