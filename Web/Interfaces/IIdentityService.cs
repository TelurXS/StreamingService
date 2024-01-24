using Application.Features.Users;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Models.Responses;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IIdentityService
{
	Task<CreateResult<Success>> RegisterAsync(RegisterRequest request);

	Task<UpdateResult<Success>> ConfirmEmailAsync(string userId, string code);

	Task<GetAllResult<Genre>> GetFavouriteGenresAsync();

	Task<UpdateResult<Success>> SetFavouriteGenresAsync(SetFavouriteGenresToUser.Request request);

	Task<GetResult<UserResponse>> GetProfileAsync();

	Task<UpdateResult<Success>> UpdateProfileAsync(UpdateUserProfile.Request request);
}
