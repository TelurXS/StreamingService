using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Accounts;

public class GetAccountByEmail
{
    public class Request : IRequest<GetResult<Account>>
    {
        public required string Email { get; set; }
    }

    public class Handler : SyncRequestHandler<Request, GetResult<Account>>
    {
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }

        private IAccountService AccountService { get; }

        protected override GetResult<Account> Handle(Request request)
        {
            return AccountService.FindByEmail(request.Email);
        }
    }
}