using AuthApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthApi.Configurations
{
    public static class CustomAuthentication
    {
        public static void AddCustomAuthentication(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.IncludeErrorDetails=true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    
                    /*ValidIssuer = Configuration["JwtConfiguration:Issuer"],
                    ValidAudience = Configuration["JwtConfiguration:Audience"],*/
                    //RequireAudience= false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfiguration:TokenSecret"]!))
                    //ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}
