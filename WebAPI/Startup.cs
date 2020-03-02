using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistance;
using WebAPI.Middleware;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Linq;
using NSwag.Generation.Processors.Security;
using NSwag;

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
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithExposedHeaders("WWW-Authenticate")
                          .WithOrigins("http://localhost:3000")
                          .AllowCredentials();
                });
            });

            services.AddInfrastructure(Configuration);
            services.AddPersistence(Configuration.GetConnectionString("DefaultConnection"));
            services.AddApplication(Configuration);

            services.AddLogging();
         
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
                        Description = "Copy this into the value field: Bearer {token}"
                    }
                );
            });
            services.AddControllers(opt => {
                //This will give authorization to every api including the login, you have to use allowanonymous///
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
                app.UseSwaggerUi3(settings =>{
                    settings.DocExpansion = "Full";
                    settings.DocumentTitle = "TemplateProjet";
                    settings.EnableTryItOut = true;
                });
                //app.UseReDoc();
                

            }

            app.UseHealthChecks("/health");
            //app.UseHttpsRedirection();


            app.UseRouting();
            app.UseCors("CorsPolicy");
            //who are you?
            app.UseAuthentication();
            //are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
