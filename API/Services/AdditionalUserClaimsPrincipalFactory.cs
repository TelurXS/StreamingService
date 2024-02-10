using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace API.Services;

public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public AdditionalUserClaimsPrincipalFactory(
        UserManager<User> userManager, 
        RoleManager<Role> roleManager, 
        IOptions<IdentityOptions> options) 
        : base(userManager, roleManager, options)
    {
    }

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;

        var nameClaim = identity.FindFirst(ClaimTypes.Name);
        identity.RemoveClaim(nameClaim);

        var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Name)
		};

        identity.AddClaims(claims);
        return principal;
    }
}
