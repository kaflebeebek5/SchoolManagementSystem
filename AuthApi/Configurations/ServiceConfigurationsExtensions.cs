using AuthApi.DbContext;
using AuthApi.Repositories.Implementation;
using AuthApi.Repositories.Interface;
using AuthApi.Services.Implementation;
using AuthApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Shared.Permission;


namespace AuthApi.Configurations
{
    public static class ServiceConfigurationsExtensions
    {
        internal static IServiceCollection AddCustomServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<SchoolManagementDbContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
                new string[]{}
                    }
                });
            });
            return services;

        }
    }
}
