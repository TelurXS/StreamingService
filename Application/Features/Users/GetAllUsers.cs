﻿using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class GetAllUsers
{
    public class Request : IRequest<GetAllResult<User>>
    {
        public int Count { get; set; } = 10;

        public int Page { get; set; } = 0;
    }

    public class Handler : SyncRequestHandler<Request, GetAllResult<User>>
    {
        public Handler(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        protected override GetAllResult<User> Handle(Request request)
        {
            return UserService.FindAll(request.Count, request.Page);
        }
    }
}
