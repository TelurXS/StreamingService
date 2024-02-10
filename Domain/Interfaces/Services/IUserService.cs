﻿using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IUserService : IEntityService<User>
{
	UpdateResult<Success> SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	UpdateResult<Success> SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);

	UpdateResult<Success> AddViewRecord(Guid id, ViewRecord viewRecord);

	GetAllResult<Title> FindFavouriteTitlesById(Guid id);

	UpdateResult<Success> AddTitleToFavourite(Guid id, Title title);

	UpdateResult<Success> RemoveTitleFromFavourite(Guid id, Title title);
}
