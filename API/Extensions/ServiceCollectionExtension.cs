using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder();
        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(x =>
            {
                x.ClientId = configuration["Authentication:Google:ClientId"]!;
                x.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
            });

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<DataContext>()
            .AddApiEndpoints();
        
        return services;
    }
    
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
                corsBuilder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(configuration["WebClientUrl"]!));
        });
        
        return services;
    }
    
    public static WebApplication MapIdentity(this WebApplication app)
    {
        app.UseAuthorization();
        app.MapIdentityApi<User>();
        
        return app;
    }
}