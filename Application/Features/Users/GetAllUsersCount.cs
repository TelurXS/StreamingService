using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class GetAllUsersCount
{
    public class Request : IRequest<int>
    {
    }

    public class Handler : SyncRequestHandler<Request, int>
    {
        public Handler(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        protected override int Handle(Request request)
        {
            return UserService.Count();
        }
    }
}
