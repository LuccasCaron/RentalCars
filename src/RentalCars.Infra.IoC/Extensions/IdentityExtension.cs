using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentalCars.Application.Configuration;
using RentalCars.Infra.Data.Context;
using System.Text;

namespace RentalCars.Infra.IoC.Extensions;

internal static class IdentityExtension
{

    public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        var jwtSettingsSection = configuration.GetSection("JwtSettings");

        services.Configure<JwtSettings>(jwtSettingsSection);

        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

        var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);

        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.RequireHttpsMetadata = true;
            opts.SaveToken = true;
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audiencia,
                ValidIssuer = jwtSettings.Emissor
            };
        });

        return services;
    }

}
