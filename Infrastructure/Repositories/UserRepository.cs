﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
	public UserRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public User? FindById(Guid id)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.Include(x => x.FavouriteTitles)
			.Include(x => x.ViewRecords)
			.Include(x => x.Lists)
			.FirstOrDefault(x => x.Id == id);
	}

	public User? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.Include(x => x.FavouriteTitles)
			.Include(x => x.ViewRecords)
			.Include(x => x.Lists)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<User> FindAll(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.Include(x => x.FavouriteTitles)
			.Include(x => x.ViewRecords)
			.Include(x => x.Lists)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<User> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.Include(x => x.FavouriteTitles)
			.Include(x => x.ViewRecords)
			.Include(x => x.Lists)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<User> FindAllByName(string name, int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.Include(x => x.FavouriteTitles)
			.Include(x => x.ViewRecords)
			.Include(x => x.Lists)
			.Where(x => x.Name.Contains(name) || x.FirstName.Contains(name) || x.SecondName.Contains(name))
			.Skip(page * count)
			.Take(count)
			.ToList();
	}


	public List<Title> FindFavouriteTitlesById(Guid id)
	{
		var user = Entities
			.Include(x => x.FavouriteTitles)
				.ThenInclude(x => x.Names)
			.Include(x => x.FavouriteTitles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.FavouriteTitles)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return new();

		return user.FavouriteTitles.ToList();
	}

	public List<User> FindFollowersFromUser(Guid id)
	{
		var user = Entities
			.Include(x => x.Followers)
			.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return new();

		return user.Followers.ToList();
	}

	public List<User> FindReadersFromUser(Guid id)
	{
		var user = Entities
			.Include(x => x.Readers)
			.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return new();

		return user.Readers.ToList();
	}

	public List<Notification> FindNotificationsFromUser(Guid id)
	{
		var user = Entities
			.Include(x => x.Notifications)
			.ThenInclude(x => x.RelatedUser)
			.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return new();

		return user.Notifications.ToList();
	}

	public bool AddUserToFollowers(Guid followerId, Guid userId)
	{
		var user = Entities
			.Include(x => x.Followers)
			.FirstOrDefault(x => x.Id == userId);

		if (user is null)
			return false;

		var follower = Entities
			.Include(x => x.Readers)
			.FirstOrDefault(x => x.Id == followerId);

		if (follower is null)
			return false;

		user.Followers.Add(follower);
		follower.Readers.Add(user);
		return Context.SaveChanges() > 0;
	}

	public bool RemoveUserFromFollowers(Guid followerId, Guid userId)
	{
		var user = Entities
			.Include(x => x.Followers)
			.FirstOrDefault(x => x.Id == userId);

		if (user is null)
			return false;

		var follower = Entities
			.Include(x => x.Readers)
			.FirstOrDefault(x => x.Id == followerId);

		if (follower is null)
			return false;

		user.Followers.Remove(follower);
		follower.Readers.Remove(user);
		return Context.SaveChanges() > 0;
	}

	public bool SetFavouriteGenres(Guid id, IEnumerable<Genre> genres)
	{
		var user = FindByIdWithTracking(id);

		if (user is null)
			return false;

		user.FavouriteGenres.Clear();

		foreach (var genre in genres)
			user.FavouriteGenres.Add(genre);

		return Context.SaveChanges() > 0;
	}

	public bool SetSubscription(Guid id, Subscription subscription, DateTime expiresIn)
	{
		var user = FindByIdWithTracking(id);

		if (user is null)
			return false;

		user.Subscription = subscription;
		user.SubscriptionExpiresIn = expiresIn;

		return Context.SaveChanges() > 0;
	}

	public bool AddViewRecord(Guid id, ViewRecord viewRecord)
	{
		var user = FindByIdWithTracking(id);

		if (user is null)
			return false;

		user.ViewRecords.Add(viewRecord);

		return Context.SaveChanges() > 0;
	}

	public bool AddTitleToFavourite(Guid id, Title title)
	{
		var user = FindByIdWithTracking(id);

		if (user is null)
			return false;

		user.FavouriteTitles.Add(title);

		return Context.SaveChanges() > 0;
	}

	public bool RemoveTitleFromFavourite(Guid id, Title title)
	{
		var user = FindByIdWithTracking(id);

		if (user is null)
			return false;

		user.FavouriteTitles.Remove(title);

		return Context.SaveChanges() > 0;
	}

	public User? Insert(User value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

	public bool Update(Guid id, User value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Email, x => value.Email)
				.SetProperty(x => x.PhoneNumber, x => value.PhoneNumber)
				.SetProperty(x => x.UserName, x => value.UserName)
				.SetProperty(x => x.Name, x => value.Name)
				.SetProperty(x => x.FirstName, x => value.FirstName)
				.SetProperty(x => x.SecondName, x => value.SecondName)
				.SetProperty(x => x.ProfileImage, x => value.ProfileImage)
				.SetProperty(x => x.BirthDate, x => value.BirthDate)
				.SetProperty(x => x.IsTrialSubscriptionUsed, x => value.IsTrialSubscriptionUsed)
				.SetProperty(x => x.SubscriptionExpiresIn, x => value.SubscriptionExpiresIn));

		return result > 0;
	}

	public bool Delete(User value)
	{
		Entities.Remove(value);
		return Context.SaveChanges() > 0;
	}

	public int Count()
	{
		return Entities.Count();
	}

	public int CountByName(string name)
	{
		return Entities
			.Where(x => x.Name.Contains(name) || x.FirstName.Contains(name) || x.SecondName.Contains(name))
			.Count();
	}
}
