using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Roles;

public static class CreateRole
{
    public class Request : IRequest<CreateResult<Role>>
    {

    }
}
