using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Sharpnado.MaterialFrame;
using VideoDemos.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Handlers;
using VideoDemos.Views;
using Xe.AcrylicView;

namespace VideoDemos;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                fonts.AddFont("Inter", "Inter-V.ttf");
                fonts.AddFont("Inter-bold.ttf", "Inter-Bold.otf");
            })
            .UseAcrylicView()
            .ConfigureMauiHandlers(handlers => { handlers.AddHandler(typeof(Video), typeof(VideoHandler)); });

        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddTransient<AuthPage>();
        builder.Services.AddTransient<ProfilePage>();
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
        return builder.Build();
    }
}