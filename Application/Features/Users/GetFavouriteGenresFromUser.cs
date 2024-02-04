using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class GetFavouriteGenresFromUser
{
	public class Request : IRequest<GetAllResult<Genre>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Genre>>
	{
		public Handler(IUserService userService)
		{
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetAllResult<Genre> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			return userResult
				.AsFound
				.FavouriteGenres
				.ToList();
		}
	}
}
