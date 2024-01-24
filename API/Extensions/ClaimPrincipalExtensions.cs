using System.Security.Claims;

namespace API.Extensions;

public static class ClaimPrincipalExtensions
{
	public static Guid GetIdentifier(this ClaimsPrincipal claims)
	{
		return new Guid(claims.FindFirstValue(ClaimTypes.NameIdentifier)!);
	}
}
