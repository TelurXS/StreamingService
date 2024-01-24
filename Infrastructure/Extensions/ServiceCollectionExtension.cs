using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IDescriptionRepository, DescriptionRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<INameRepository, NameRepository>();
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<ISeriesRepository, SeriesRepository>();
        services.AddScoped<ITitleRepository, TitleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IDescriptionService, DescriptionService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<INameService, NameService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<ISeriesService, SeriesService>();
        services.AddScoped<ITitleService, TitleService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}