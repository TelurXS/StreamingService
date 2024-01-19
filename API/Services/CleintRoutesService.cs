namespace API.Services;

public sealed class ClientRoutesService
{
	private const string SECTION_NAME = "ClientRoutes";

	public ClientRoutesService(IConfiguration configuration)
    {
		var section = configuration.GetSection(SECTION_NAME);

		Host = section.GetValue<string>(nameof(Host))!;
		ConfirmEmail = section.GetValue<string>(nameof(ConfirmEmail))!;
	}

    private string Host { get; }
    private string ConfirmEmail { get; }

	public string ToConfirmEmailLink(string hostConfirmationLink)
	{
		var query = new UriBuilder(hostConfirmationLink).Query;
		return $"{Host}{ConfirmEmail}{query}";
	}
}
