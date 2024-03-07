using Application.Features.Users;
using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IUserService : IWebService<User, User, UpdateUser.Request>
{
	Task<GetResult<User>> GetProfileAsync(Guid id);

	Task<GetAllResult<Genre>> GetFavouriteGenresAsync(Guid id);

	Task<GetAllResult<ViewRecord>> GetViewRecordsAsync(Guid id);

	Task<GetAllResult<Title>> GetFavouriteTitlesAsync(Guid id);

	Task<GetAllResult<TitlesList>> GetTitlesListsAsync(Guid id);

	Task<GetAllResult<User>> GetReadersAsync(Guid id);

	Task<GetAllResult<User>> GetFollowersAsync(Guid id);
}
