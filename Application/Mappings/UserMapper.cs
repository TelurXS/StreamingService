using Application.Interfaces.Mappings;
using Domain.Entities;
using Domain.Models.Responses;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class UserMapper : IUserMapper
{
	public partial UserResponse ToResponse(User user);
}
