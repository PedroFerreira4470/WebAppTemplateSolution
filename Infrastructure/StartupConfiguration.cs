using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using Infrastructure.SecurityServices;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class StartupConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtGenerator, JwtTokenGeneratorService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUriService, UriService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.SetupDbContext(configuration);
            services.SetupAuthenticationWithJwt(configuration);
            services.SetupAuthorization(configuration);
            services.SetupIdentity();


            return services;
        }
        private static void SetupAuthenticationWithJwt(this IServiceCollection services, IConfiguration configuration)
        {
            //token in user secrets (reason should not be shared)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };

                    //For signalR
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var acessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(acessToken)
                                && path.StartsWithSegments("/commentHub")
                                && path.StartsWithSegments("/notificationHub"))
                            {
                                context.Token = acessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            var authorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            services.AddControllers(opt =>
            {
                //This will give authorization to every api including the login
                var policy = authorizationPolicyBuilder;
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        private static void SetupAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            //1
            //Restricting endpoints with Claims
            //In Controller add : [Authorize(Policy = nameof(ClaimValueViewer))]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(nameof(ClaimValueViewer),
            //        builder => builder.RequireClaim("values.view", "true")
            //        );
            //});

            //2
            //Restricting endpoints with Authorization Roles.Use [Authorize(Roles = "Admin" )] or [Authorize(Roles = "Admin,OtherRole" )]
            //Other alternative You can have a policy with multiple roles
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("OverPower",
            //        builder => builder.RequireRole("Admin","Developers")
            //        );
            //});

            //3
            //Restricting endpoints with Authorization handlers. Controller use: [Authorize(Policy = nameof(WorkForHotmail))]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(nameof(WorkForHotmail),
            //        builder => builder.AddRequirements(new WorkForCompanyRequirement("hotmail.com"))
            //    );
            //});
            //services.AddSingleton<IAuthorizationHandler, WorkForCompanyRequirementHandler>();
        }

        private static void SetupDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    option => option.EnableRetryOnFailure(5)
                        .CommandTimeout(100)
                        .MigrationsAssembly(typeof(TemplateDbContext).Assembly.FullName))
                    ;
            });

            services.AddScoped<ITemplateDbContext>(provider => provider.GetService<TemplateDbContext>());
        }
        //private static void SetupContextAccessor(this IServiceCollection services)
        //{
        //    services.AddHttpContextAccessor();
        //}

        private static void SetupIdentity(this IServiceCollection services)
        {

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TemplateDbContext>()
            .AddSignInManager<SignInManager<User>>();

        }
    }


}
