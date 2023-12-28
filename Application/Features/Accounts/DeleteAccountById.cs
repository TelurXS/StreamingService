using Application.Models;
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
        protected override DeleteResult Handle(Request request)
        {
            throw new NotImplementedException();
        }
    }
}