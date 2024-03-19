using Microsoft.Maui;

namespace VideoDemos;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

#if WINDOWS
        Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("NoLabel", (handler, View) =>
        {
            handler.PlatformView.OnContent = null;
            handler.PlatformView.OffContent = null;

            // Add this to remove the padding around the switch as well
            // handler.PlatformView.MinWidth = 0;
        });
        MainPage = new AppShell();
#elif ANDROID || IOS
        MainPage = new MobileAppShell();
#endif
    }
}