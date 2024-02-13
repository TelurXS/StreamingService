using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions;

public static class ServiceCollectionExtension
{
	public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{

		//services.AddAuthentication(options =>
		//	{
		//		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		//		options.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
		//	})
		//	.AddCookie(IdentityConstants.ApplicationScheme, x =>
		//	{
		//		x.ExpireTimeSpan = TimeSpan.FromDays(1);
		//	})
		//	.AddBearerToken(IdentityConstants.BearerScheme, x => { })
		//	.AddGoogle(x =>
		//	{
		//		x.ClientId = configuration["Authentication:Google:ClientId"]!;
		//		x.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
		//	});

		services.AddIdentityCore<User>().AddRoles<Role>()
			.AddEntityFrameworkStores<DataContext>()
			.AddRoleManager<RoleManager<Role>>()
			.AddDefaultTokenProviders()
			.AddApiEndpoints();

		return services;
	}

	public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(corsBuilder =>
				corsBuilder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("http://telurxs-001-site1.ftempurl.com/")
					.AllowCredentials());
		});

		return services;
	}
}