using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Accounts;

public static class DeleteAccountById
{
    public class Request : IRequest<DeleteResult>
    {
        public Guid Id { get; set; }
    }
    
    public class Handler : SyncRequestHandler<Request, DeleteResult>
    {
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }
        
        private IAccountService AccountService { get; }
        
        protected override DeleteResult Handle(Request request)
        {
            return AccountService.DeleteById(request.Id);
        }
    }
}