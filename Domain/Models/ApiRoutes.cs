namespace Domain.Models;

public static class ApiRoutes
{
    public const string Base = "/api";

    public const string Register = Base + "/register";

    public const string Login = Base + "/login";

    public const string Logout = Base + "/logout";

    public const string Refresh = Base + "/refresh";

    public const string User = Base + "/user";

    public const string ConfirmEmail = Base + "/confirmEmail";

    public const string ResendConfirmationEmail = Base + "/confirmEmail";

	public static class Manage
	{
		public const string GroupName = "/manage";

		public const string Profile = Base + GroupName + "/profile";

		public const string Genres = Base + GroupName + "/genres";
	}

}
