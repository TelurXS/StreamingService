namespace Domain.Models;

public static class WebRoutes
{
	public const string Home = "/";

	public const string NotAuthorized = "/not-authorized";

	public const string Register = "/register";

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
}
