using Application.Features.TitleLists;
using Application.Features.Users;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IIdentityService
{
	Task<CreateResult<Success>> RegisterAsync(RegisterRequest request);

	Task<UpdateResult<Success>> ConfirmEmailAsync(string userId, string code);

	Task<GetAllResult<Genre>> GetFavouriteGenresAsync();

	Task<UpdateResult<Success>> SetFavouriteGenresAsync(SetFavouriteGenresToUser.Request request);

	Task<GetResult<User>> GetProfileAsync();

	Task<UpdateResult<Success>> UpdateProfileAsync(UpdateUserProfile.Request request);

	Task<GetAllResult<ViewRecord>> GetViewRecordsAsync();

	Task<CreateResult<Success>> RegisterViewRecordAsync(Guid seriesId, RegisterViewRecordToUser.Request request);

	Task<GetAllResult<Title>> GetFavouriteTitlesAsync();
	
	Task<UpdateResult<Success>> AddTitleToFavouriteAsync(Title title);

	Task<UpdateResult<Success>> RemoveTitleFromFavouriteAsync(Title title);

	Task<GetAllResult<TitlesList>> GetTitlesListsAsync();
}
