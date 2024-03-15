namespace Domain.Models;

public static class ApiRoutes
{
	public const string Base = "/api";

	public const string Register = Base + "/register";

	public const string Login = Base + "/login";

	public const string ExternalLogin = Base + "/external-login";

	public const string ExternalLoginCallback = Base + "/external-login-callback";

	public const string Logout = Base + "/logout";

	public const string Refresh = Base + "/refresh";

	public const string User = Base + "/user";

	public const string ConfirmEmail = Base + "/confirmEmail";

	public const string ResendConfirmationEmail = Base + "/confirmEmail";

	public const string ForgotPassword = Base + "/forgotPassword";

	public const string ResetPassword = Base + "/resetPassword";

	public static class Manage
	{
		public const string GroupName = "/manage";

		public const string Profile = Base + GroupName + "/profile";

		public const string Genres = Base + GroupName + "/genres";

		public const string ProfileImage = Base + GroupName + "/image";
	}

	public static class IdentityUsers
	{
		public const string RegisterViewRecordRoute = Base + "/register-view-record";

		public const string RegisterViewRecord = RegisterViewRecordRoute + "/{seriesId}";

		public const string RegisterRateRoute = Base + "/register-rate";

		public const string RegisterRate = RegisterRateRoute + "/{titleId}";

		public const string ViewRecords = Base + "/view-records";

		public const string Followers = Base + "/followers";

		public const string FollowersById = Base + "/followers/{userId}";

		public const string Readers = Base + "/readers";

		public const string ReadersById = Base + "/readers/{userId}";

		public const string ApplyTrial = Base + "/apply-trial";

		public const string Notifications = Base + "/notifications";

		public const string SnoozeNotifications = Notifications + "/snooze";

		public static class FavouriteTitles
		{
			public const string GroupName = "/favourites";

			public const string Route = Base + GroupName;

			public const string RouteWithId = Base + GroupName + "/{titleId}";
		}
	}

	public class Users
	{
		public const string GroupName = "/users";

		public const string All = Base + GroupName;

		public const string CountAll = All + "/count";

		public const string GroupNameWithId = GroupName + "/{id}";

		public const string Profile = Base + GroupNameWithId + "/profile";

		public const string Genres = Base + GroupNameWithId + "/genres";

		public const string ViewRecords = Base + GroupNameWithId + "/view-records";

		public const string Followers = Base + GroupNameWithId + "/followers";

		public const string Readers = Base + GroupNameWithId + "/readers";

		public const string FavouriteTitles = Base + GroupNameWithId + "/favourites";

		public const string TitlesLists = Base + GroupNameWithId + "/lists";
	}

	public static class Titles
	{
		public const string GroupName = "/titles";

		public const string Route = Base + GroupName;

		public const string All = Base + GroupName;

		public const string AllPopular = All + "/popular";

		public const string AllByType = All + "/by-type";

		public const string AllByName = All + "/by-name";

		public const string AllByLanguage = All + "/by-language";

		public const string AllByGenre = All + "/by-genre";

		public const string AllByGenres = All + "/by-genres";

		public const string AllByFilter = All + "/filter";

		public const string CountAll = All + "/count";

		public const string CountByType = AllByType + "/count";

		public const string CountByName = AllByName + "/count";

		public const string CountByLanguage = AllByLanguage + "/count";

		public const string CountByGenre = AllByGenre + "/count";

		public const string CountByGenres = AllByGenres + "/count";

		public const string CountByFilter = AllByFilter + "/count";

		public const string ById = Base + GroupName + "/{id:guid}";

		public const string ImageById = ById + "/image";

		public const string BySlug = Base + GroupName + "/{slug}";
	}

	public static class Genres
	{
		public const string GroupName = "/genres";

		public const string Route = Base + GroupName;

		public const string All = Base + GroupName;
	}

	public static class TitlesLists
	{
		public const string GroupName = "/lists";

		public const string Route = Base + GroupName;

		public const string All = Base + GroupName;

		public const string ById = Base + GroupName + "/{id}";

		public const string AddTitleToList = Base + GroupName + "/{listId}/{titleId}";

		public const string RemoveTitleFromList = Base + GroupName + "/{listId}/{titleId}";
	}

	public static class Comments
	{
		public const string GroupName = "/comments";

		public const string Route = Base + GroupName;

		public const string CreateForTitle = Base + GroupName + "/{titleId}";
	}

	public static class Files
	{
		public const string UserImages = Base + "/users/images";

		public const string UserImageByName = Base + "/users/images/{name}";

		public const string TitleImages = Base + "/titles/images";

		public const string TitleImageByName = Base + "/titles/images/{name}";

		public const string TitleScreenshots = Base + "/titles/screenshots";

		public const string TitleScreenshotByName = Base + "/titles/screenshots/{name}";

		public const string Series = Base + "/series";

		public const string SeriesByName = Base + "/series/{name}";
	}

	public class Rates
	{
		public const string GroupName = "/rates";

		public const string Route = Base + GroupName;

		public const string ByTitle = Route + "/{titleId}";
	}

	public static class Series
	{
		public const string GroupName = "/series";

		public const string Route = Base + GroupName;

		public const string All = Route;

		public const string CountAll = Route + "/count";

		public const string ById = Route + "/{id}";
	}

	public class Payment
	{
		public const string GroupName = "/payment";

		public const string Route = Base + GroupName;

		public const string CaptureOrder = Route;

		public const string CreateOrder = Route + "/{subscription}";
	}

	public class Subscriptions
	{
		public const string GroupName = "/subscriptions";

		public const string Route = Base + GroupName;

		public const string All = Route;

		public const string ById = Route + "/{id:guid}";

		public const string ByName = Route + "/{name}";
	}

	public class Notifications
	{
		public const string GroupName = "/notifications";

		public const string Route = Base + GroupName;

		public const string All = Route;

        public const string Invite = Route + "/invite";

        public const string InviteById = Invite + "/{id:guid}";
	}

	public class Hubs
	{
		public const string Sessions = Base + "/sessions";
	}
}
