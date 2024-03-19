using Application.Features.Accounts;
using Application.Interfaces;
using Application.Interfaces.Mappings;
using Application.Mappings;
using Application.Models;
using Domain.Interfaces.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAccountMapper, AccountMapper>();
        services.AddTransient<IGenreMapper, GenreMapper>();
        services.AddTransient<IImageMapper, ImageMapper>();
        services.AddTransient<IDescriptionsMapper, DescriptionMapper>();
        services.AddTransient<INameMapper, NameMapper>();
        services.AddTransient<IRateMapper, RateMapper>();
        services.AddTransient<ISeriesMapper, SeriesMapper>();
        services.AddTransient<ITitleMapper, TitleMapper>();
        services.AddTransient<IUserMapper, UserMapper>();
        services.AddTransient<ITitlesListMapper, TitlesListMapper>();
        services.AddTransient<IResponseMapper, ResponseMapper>();
        services.AddTransient<INotificationMapper, NotificationMapper>();

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
        
        services.AddMediatR(x => 
            x.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        return services;
    } 
}