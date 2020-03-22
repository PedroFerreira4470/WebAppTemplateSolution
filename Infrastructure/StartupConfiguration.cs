using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IJwtGenerator, JwsGenerator>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            //TODO Change AddDbContext into AddDbContextPool (Problem: constructor schema)
            services.AddDbContext<TemplateDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                , b => b.MigrationsAssembly(typeof(TemplateDbContext).Assembly.FullName));
            });

            services.AddScoped<ITemplateDbContext>(provider => provider.GetService<TemplateDbContext>());

            var builder = services.AddIdentityCore<User>();
            var identitybuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identitybuilder.AddEntityFrameworkStores<TemplateDbContext>();
            identitybuilder.AddSignInManager<SignInManager<User>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"] /*TODO use user secrets*/));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

            return services;
        }
    }
}
