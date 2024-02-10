namespace Domain.Models;

public static class WebRoutes
{
	public const string Home = "/";

	public const string NotAuthorized = "/not-authorized";

	public const string Register = "/register";

	public const string ConfirmationSent = "/confirmation-sent";

	public const string RegistrationSuccess = "/registration-success";

	public const string Login = "/login";

	public const string Logout = "/logout";

	public const string ConfirmEmail = "/confirm-email";

	public static class Manage
	{
		public const string GroupName = "/manage";

		public const string Profile = GroupName + "/profile";

		public const string Genres = GroupName + "/genres";

		public const string Subscription = GroupName + "/subscription";
	}

	public static class Titles
	{
		public const string Base = "/titles";

		public const string Title = Base + "/{slug}";
	}

	public static class My
	{
		public const string History = "/history";	

		public const string Lists = "/lists";	

		public static class Profile
		{
			public const string Base = "/profile";

			public const string MyProfile = Base;

			public const string Favourites = Base + "/favourites";
		}
	}

	public static class Users
	{
		public static class Profile
		{
			public const string Base = "/profile";

			public const string ById = Base + "/{id}";
		}
	}

	public static class TitlesLists 
	{
		public const string Base = "/lists";

		public const string ById = Base + "/{id}";
	}
}
