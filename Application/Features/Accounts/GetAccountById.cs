using Application.Models;
using Azure;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Accounts;

public static class GetAccountById
{
    public class Request : IRequest<GetResult<Account>>
    {
        public Guid Id { get; set; }
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
            return AccountService.FindById(request.Id);
        }
    }
}