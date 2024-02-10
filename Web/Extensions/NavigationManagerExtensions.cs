using Microsoft.AspNetCore.Components;
using System.Security.Policy;

namespace Web.Extensions;

public static class NavigationManagerExtensions
{
	public static string GetCurrentPage(this NavigationManager manager)
	{
		return manager.ToAbsoluteUri(manager.Uri).GetLeftPart(UriPartial.Path);
	}

    public static string GetCurrentRoute(this NavigationManager manager)
    {
		return manager.ToAbsoluteUri(manager.Uri).PathAndQuery;
    }

    public static void NavigateToCurrentPage(this NavigationManager manager)
	{
		manager.NavigateTo(manager.GetCurrentPage());
	}
}
