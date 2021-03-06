﻿using Application;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebAPI.Versioning;

namespace WebAPI
{
    public static class StartupConfigurationServices
    {
        public static IServiceCollection AddCustomConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.AddHealthChecks().AddDbContextCheck<TemplateDbContext>();
            services.SetupCors();
            services.SetupSwaggerDoc();
            services.SetupAPIVersioning();


            return services;
        }

        private static void SetupCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("WWW-Authenticate")
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });
        }

        private static void SetupSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Template API", Version = "v1" });
                x.SwaggerDoc("v2", new OpenApiInfo { Title = "Template API", Version = "v2" });
                x.ResolveConflictingActions(x => x.First());
                x.OperationFilter<RemoveVersionFromParameter>();
                x.DocumentFilter<ReplaceSwaggerVersionWithExactValueInPath>();
                x.ExampleFilters();
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

                var xmlFileApplication = $"{typeof(StartupConfiguration).Assembly.GetName().Name}.xml";
                var xmlPathApplication = Path.Combine(AppContext.BaseDirectory, xmlFileApplication);
                x.IncludeXmlComments(xmlPathApplication);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);


            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        private static void SetupAPIVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

        }

    }
}
