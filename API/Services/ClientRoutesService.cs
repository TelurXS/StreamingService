using Domain.Models;

namespace API.Services;

public sealed class ClientRoutesService
{
	private const string URL_NAME = "Client:Url";

	public ClientRoutesService(
		IConfiguration configuration,
		IWebHostEnvironment enviroment)
    {
		Enviroment = enviroment;

		Host = configuration.GetValue<string>(URL_NAME)!;
	}

	private string Host { get; }

	public IWebHostEnvironment Enviroment { get; }

	public string ToConfirmEmailLink(string hostConfirmationLink)
	{
		var query = new UriBuilder(hostConfirmationLink).Query;
		return $"{Host}{WebRoutes.ConfirmEmail}{query}";
	}

	public string ToResetPasswordLink(string code, string email)
	{
		return $"{Host}{WebRoutes.ResetPassword}?code={code}&email={email}";
	}
}
