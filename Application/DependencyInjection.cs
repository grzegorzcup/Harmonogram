using Application.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Middleware;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication (this IServiceCollection services, IConfiguration configuration)
        {
            var authenticatrionSettings = new AuthenticationSettings();
            configuration.GetSection("JWTConfig").Bind(authenticatrionSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticatrionSettings.JWTIssuer,
                    ValidAudience = authenticatrionSettings.JWTIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticatrionSettings.Secret))
                };
            });

            services.AddSingleton(authenticatrionSettings);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService,LoginService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}
