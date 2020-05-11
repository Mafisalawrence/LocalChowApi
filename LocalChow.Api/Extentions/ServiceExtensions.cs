using LocalChow.Api.Settings;
using LocalChow.Domain;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Xml.Xsl;

namespace LocalChow.Api.Extentions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<LocalChowDbContext>()
                .AddDefaultTokenProviders();
        }
        public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration confugaration)
        {
            services.AddDbContext<LocalChowDbContext>(options => options.UseSqlServer(confugaration.GetConnectionString("DbConnection")));
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationConfiguration = new AuthenticationConfiguration();
                configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authenticationConfiguration);
            var key = Encoding.ASCII.GetBytes(authenticationConfiguration.SecreteKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey =true,
                    
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = authenticationConfiguration.ValidIssuer,
                    ValidAudience =authenticationConfiguration.ValidAudience
                };
             });
        }
    }
}
