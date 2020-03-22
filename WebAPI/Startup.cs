using Application;
using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;
using System.Linq;
using WebAPI.Middleware;
using WebAPI.SignalR;

namespace WebApplicationTemplate
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

            var conf = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(conf)
                .CreateLogger();

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

            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddSignalR();
            services.AddHealthChecks()
               .AddDbContextCheck<TemplateDbContext>();

            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "OpenAPI";
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}",

                    }
                );

                // Post process the generated document
                config.PostProcess = d =>
                {
                    d.Info.Title = "TemplateProjet";
                    d.Info.Description = "Description goes here";
                    d.Info.License = new OpenApiLicense();
                    d.Info.Version = "V1";
                };

                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });




            services.AddControllers(opt =>
            {
                //This will give authorization to every api including the login
                //you have access to controller without authentication you need to use D.A [AllowAnonymous]
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICurrentUserService>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3(settings =>
                {
                    settings.Path = "/api";
                    settings.DocExpansion = "Full";
                    settings.DocumentTitle = "TemplateProjet";
                    settings.EnableTryItOut = true;
                });

            }


            app.UseHealthChecks("/health");
            //app.UseHttpsRedirection();
            //app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CommentHub>("/commentHub");
                endpoints.MapHub<NotificationHub>("/notificationHub");
                /*fallback goes here*/
            });

        }
    }
}
