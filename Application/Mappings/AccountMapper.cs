using Application.Features.Accounts;
using Application.Interfaces;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class AccountMapper : IAccountMapper
{
    public partial Account FromRequest(CreateAccount.Request request);

    public partial Account FromRequest(UpdateAccount.Request request);
}