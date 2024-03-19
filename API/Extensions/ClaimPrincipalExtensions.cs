using System.Security.Claims;

namespace API.Extensions;

public static class ClaimPrincipalExtensions
{
	public static Guid GetIdentifier(this ClaimsPrincipal claims)
	{
		var id = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        if (id is null)
			return Guid.Empty;

        return new Guid(id);
	}
}
