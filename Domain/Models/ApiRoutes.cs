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

    public static class Titles
    {
		public const string GroupName = "/titles";
		
        public const string Route = Base + GroupName;

		public const string All = Base + GroupName;

        public const string ById = Base + GroupName + "/{id}";

        public const string BySlug = Base + GroupName + "/{slug}";
	}

	public static class Comments
	{
		public const string GroupName = "/comments";

		public const string CreateForTitle = Base + GroupName + "/{titleId}";
	}
}
