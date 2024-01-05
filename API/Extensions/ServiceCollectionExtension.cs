using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder();
        
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme)
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
    
    public static WebApplication MapIdentity(this WebApplication app)
    {
        app.UseAuthorization();
        app.MapIdentityApi<User>();
        
        return app;
    }
}