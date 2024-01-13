using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Role : IdentityRole<Guid>
{
	public static string User => nameof(User);
	public static string Moderator => nameof(Moderator);
	public static string Admin => nameof(Admin);
}
