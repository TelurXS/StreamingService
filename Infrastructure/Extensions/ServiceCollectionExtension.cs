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
        var connection = configuration.GetConnectionString("Default")!;
        var version = new MySqlServerVersion(new Version(8, 0, 35));

		services.AddDbContext<DataContext>(options =>
            options.UseMySql(connection, version));

        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IDescriptionRepository, DescriptionRepository>();
        services.AddTransient<IGenreRepository, GenreRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<INameRepository, NameRepository>();
        services.AddTransient<IRateRepository, RateRepository>();
        services.AddTransient<ISeriesRepository, SeriesRepository>();
        services.AddTransient<ITitleRepository, TitleRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IViewRecordRepository, ViewRecordRepository>();
        services.AddTransient<ITitlesListRepository, TitlesListRepository>();
        services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();

        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IDescriptionService, DescriptionService>();
        services.AddTransient<IGenreService, GenreService>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<INameService, NameService>();
        services.AddTransient<IRateService, RateService>();
        services.AddTransient<ISeriesService, SeriesService>();
        services.AddTransient<ITitleService, TitleService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<IViewRecordService, ViewRecordService>();
        services.AddTransient<ITitlesListService, TitlesListService>();
        services.AddTransient<ISubscriptionService, SubscriptionService>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}