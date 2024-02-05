﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class CommentService : ICommentService
{
    public CommentService(ICommentRepository repository)
    {
		Repository = repository;
	}

	private ICommentRepository Repository { get; }

	public GetResult<Comment> FindById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Comment> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Comment> FindAll()
	{
		return Repository.FindAll();
	}

	public GetAllResult<Comment> FindAllWithTracking()
	{
		return Repository.FindAllWithTracking();
	}

	public CreateResult<Comment> Create(Comment value)
	{
		var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

	public UpdateResult<Comment> Update(Guid id, Comment value)
	{
		if (Repository.FindById(id) is null)
			return new NotFound();

		if (Repository.Update(id, value) is false)
			return new Failed();

		var entity = Repository.FindById(id);

		if (entity is null)
			return new Failed();

		return entity;
	}

	public DeleteResult DeleteById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return Delete(result);
	}

	public DeleteResult Delete(Comment value)
	{
		var result = Repository.Delete(value);

		if (result is false)
			return new Failed();

		return new Success();
	}
}