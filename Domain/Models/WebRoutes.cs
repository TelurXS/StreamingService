using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Domain.Models;

public static class WebRoutes
{
	public const string Home = "/";

	public const string NewAndPopular = "/new-and-popular";

	public const string Series = "/series";

	public const string Films = "/films";

	public const string WatchViaLanguages = "/languages";

	public const string NotAuthorized = "/not-authorized";

	public const string Payment = "/payment";

	public const string PaymentByName = Payment + "/{selected}";

	public const string ApplyTrial = "/apply-trial";

	public const string Register = "/register";

	public const string ConfirmationSent = "/confirmation-sent";

	public const string RegistrationSuccess = "/registration-success";

	public const string ResetCodeSent = "/reset-code-sent";

	public const string Login = "/login";

	public const string Logout = "/logout";

	public const string ConfirmEmail = "/confirm-email";

	public const string ForgotPassword = "/forgot-password";

	public const string ResetPassword = "/reset-password";

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

	public static class Settings
	{
		public const string Base = "/settings";

		public const string ChangePassword = Base + "/change-password";
	}

	public static class Users
	{
		public const string Base = "/users";

		public static class Profile
		{
			public const string ById = Base + "/{id}";
		}
	}

	public static class TitlesLists 
	{
		public const string Base = "/lists";

		public const string ById = Base + "/{id}";
	}

	public static class Sessions
	{
		public const string Base = "/sessions";

		public const string ByTitleAndId = Base + "/{slug}/{sessionId}";
	}

	public static class Dashboard
	{
		public const string Base = "/dashboard";

		public const string Home = Base + "/home";

		public const string Titles = Base + "/titles";

		public const string Series = Base + "/series";

		public const string Users = Base + "/users";
	}

	public static class Info
	{
		public const string Base = "";

		public const string HelpCenter = Base + "/help-center";

		public const string Faq = Base + "/faq";

		public const string LegalNotices = Base + "/legal-notices";

		public const string PrivacyPolicy = Base + "/privacy-policy";
	}
}
