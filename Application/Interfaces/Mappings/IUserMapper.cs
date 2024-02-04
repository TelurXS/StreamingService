using Domain.Entities;
using Domain.Models.Responses;

namespace Application.Interfaces.Mappings;

public interface IUserMapper
{
	UserResponse ToResponse(User value);
}
