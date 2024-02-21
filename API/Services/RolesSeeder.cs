
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public sealed class RolesSeeder : BackgroundService
{
    public RolesSeeder(IServiceScopeFactory serviceScopeFactory)
    {
		ServiceScopeFactory = serviceScopeFactory;

		Roles = [
			Role.User, 
			Role.Moderator, 
			Role.Admin
			];
	}

	private IServiceScopeFactory ServiceScopeFactory { get; }
	private List<string> Roles { get; } 

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		using var scope = ServiceScopeFactory.CreateScope();
		var roleStore = scope.ServiceProvider.GetRequiredService<IRoleStore<Role>>();

		foreach (var name in Roles)
		{
			if (await roleStore.FindByNameAsync(name, stoppingToken) is not null)
				continue;

			var role = new Role { Name = name, NormalizedName = name.ToUpper() };

			await roleStore.CreateAsync(role, stoppingToken);
		}
	}
}
