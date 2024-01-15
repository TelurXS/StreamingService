using Microsoft.AspNetCore.Components;

namespace Web.Extensions;

public static class NavigationManagerExtension
{
    public static string GetCurrentPage(this NavigationManager navigationManager)
    {
        return navigationManager
            .ToAbsoluteUri(navigationManager.Uri)
            .GetLeftPart(UriPartial.Path);
    }

    public static void NavigateToCurrentPage(this NavigationManager navigationManager) 
    {
        navigationManager.NavigateTo(navigationManager.GetCurrentPage());
    }
}
