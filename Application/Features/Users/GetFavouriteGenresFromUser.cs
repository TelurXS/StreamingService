using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class GetFavouriteGenresFromUser
{
	public class Request : IRequest<GetAllResult<Response>>
	{
		public Guid UserId { get; set; } = default;
	}

	public class Response
	{
		public Guid Id { get; set; }

		public required string Name { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Response>>
	{
		public Handler(IUserService userService)
		{
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetAllResult<Response> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			return userResult
				.AsFound
				.FavouriteGenres
				.Select(x => new Response { Id = x.Id, Name = x.Name })
				.ToList();
		}
	}
}
