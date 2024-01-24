using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Responses;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class GetUserById
{
	public class Request : IRequest<GetResult<UserResponse>>
	{
		public required Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetResult<UserResponse>>
	{
        public Handler(IUserService userService, IUserMapper mapper)
        {
			UserService = userService;
			Mapper = mapper;
		}

		private IUserService UserService { get; }
		private IUserMapper Mapper { get; }

		protected override GetResult<UserResponse> Handle(Request request)
		{
			var result = UserService.FindById(request.Id);

			if (result.IsFound is false)
				return new NotFound();

			return Mapper.ToResponse(result.AsFound);
		}
	}
}
