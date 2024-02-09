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

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}