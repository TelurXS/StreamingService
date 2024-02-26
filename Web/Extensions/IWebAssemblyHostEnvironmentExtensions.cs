using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Web.Extensions;

public static class IWebAssemblyHostEnvironmentExtensions
{
	public static string GetHostName(this IWebAssemblyHostEnvironment environment)
	{
		var length = environment.BaseAddress.Length;

		if (length < 0)
			return string.Empty;

		return environment.BaseAddress.Remove(length - 1);
	}
}
