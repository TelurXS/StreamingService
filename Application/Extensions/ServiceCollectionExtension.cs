using Application.Interfaces;
using Application.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAccountMapper, AccountMapper>();
        
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
        
        services.AddMediatR(x => 
            x.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        return services;
    } 
}