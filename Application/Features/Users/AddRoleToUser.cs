using Application.Features.Rates;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class AddRoleToUser
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;

		public string Role { get; set; } = string.Empty;
	}

	public class Handler : IRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(IUserService userService, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
			UserService = userService;
			RoleManager = roleManager;
			UserManager = userManager;
		}

		private IUserService UserService { get; }
		private RoleManager<Role> RoleManager { get; }
		private UserManager<User> UserManager { get; }

		public async Task<UpdateResult<Success>> Handle(Request request, CancellationToken cancellationToken)
		{
			var userResult = UserService.FindByIdWithTracking(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;

			var role = await RoleManager.FindByNameAsync(request.Role);

			if (role is null)
				return new Failed();

			var result = await UserManager.AddToRoleAsync(user, request.Role);

			if (result.Succeeded)
				return new Success();

			return new Failed();
		}
	}
}
