using Application.Features.Accounts;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAccountMapper
{
    Account FromRequest(CreateAccount.Request request);
    
    Account FromRequest(UpdateAccount.Request request);
}