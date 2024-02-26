using Application.Features.Users;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Microsoft.AspNetCore.Components.Forms;

namespace Web.Interfaces;

public interface IIdentityService
{
	Task<CreateResult<Success>> RegisterAsync(RegisterRequest request);

	Task<UpdateResult<Success>> ConfirmEmailAsync(string userId, string code);

	Task<UpdateResult<Success>> ForgotPassword(string email);

	Task<UpdateResult<Success>> ResetPassword(ResetPasswordRequest request);

	Task<GetAllResult<Genre>> GetFavouriteGenresAsync();

	Task<UpdateResult<Success>> SetFavouriteGenresAsync(SetFavouriteGenresToUser.Request request);

	Task<GetResult<User>> GetProfileAsync();

	Task<UpdateResult<Success>> UpdateProfileAsync(UpdateUserProfile.Request request);

	Task<GetAllResult<ViewRecord>> GetViewRecordsAsync();

	Task<CreateResult<Success>> RegisterViewRecordAsync(Guid seriesId, RegisterViewRecordToUser.Request request);

	Task<CreateResult<Success>> RegisterRateAsync(Guid titleId, RegisterRateToTitleFromUser.Request request);

	Task<GetAllResult<Title>> GetFavouriteTitlesAsync();
	
	Task<UpdateResult<Success>> AddTitleToFavouriteAsync(Title title);

	Task<UpdateResult<Success>> RemoveTitleFromFavouriteAsync(Title title);

	Task<GetAllResult<TitlesList>> GetTitlesListsAsync();

	Task<UpdateResult<Success>> SetProfileImageAsync(IBrowserFile file);

	Task<UpdateResult<Success>> RemoveProfileImageAsync();

	Task<GetAllResult<User>> GetFollowersAsync();

	Task<GetAllResult<User>> GetReadersAsync();

	Task<UpdateResult<Success>> FollowAsync(Guid userId);

	Task<UpdateResult<Success>> UnfollowAsync(Guid userId);

	Task<UpdateResult<Success>> AppyTrialSubscription();
}
