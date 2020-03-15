using Application._Interfaces;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistance;

namespace Infrastructure
{
    public static class StartupConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtGenerator, JwsGenerator>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();


            var builder = services.AddIdentityCore<User>();
            var identitybuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identitybuilder.AddEntityFrameworkStores<TemplateDbContext>();
            identitybuilder.AddSignInManager<SignInManager<User>>();
            //Give token key to the Authentication
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"] /*try to use user secrets*/));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
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
                        OnMessageReceived = context => {
                            var acessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(acessToken) &&
                                (path.StartsWithSegments("/chat")))
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
