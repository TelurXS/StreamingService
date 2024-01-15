using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Roles;

public static class UpdateRole
{
    public class Request : IRequest<UpdateResult<Role>>
    {

    }
}
