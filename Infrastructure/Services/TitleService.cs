using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class TitleService : ITitleService
{
    public TitleService(ITitleRepository repository)
    {
        Repository = repository;
    }
    
    private ITitleRepository Repository { get; }
    
    public GetResult<Title> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Title> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Title> FindBySlug(string slug)
	{
		var result = Repository.FindBySlug(slug);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Title> FindBySlugWithTracking(string slug)
	{
		var result = Repository.FindBySlugWithTracking(slug);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Title> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
	}

	public GetAllResult<Title> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public GetAllResult<Title> FindAllPopular(int count = 10, int page = 0)
	{
		return Repository.FindAllPopular(count, page);
	}

	public GetAllResult<Title> FindAllByName(string name, int count = 10, int page = 0)
	{
		return Repository.FindAllByName(name, count, page);
	}

	public GetAllResult<Title> FindAllByGenre(string genre, int count = 10, int page = 0)
	{
		return Repository.FindAllByGenre(genre, count, page);
	}

	public GetAllResult<Title> FindAllByGenres(List<string> genres, int count = 10, int page = 0)
	{
		return Repository.FindAllByGenres(genres, count, page);
	}

	public UpdateResult<Success> AddView(Guid id, int count = 1)
	{
		var result = Repository.AddView(id, count);

		if (result is false)
			return new Failed();

		return new Success();
	}

	public CreateResult<Title> Create(Title value)
    {
        var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Title> Update(Guid id, Title value)
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

	public DeleteResult Delete(Title value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public UpdateResult<Success> SetImage(Guid id, Image image)
    {
        var result = Repository.SetImage(id, image);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public UpdateResult<Success> AddScreenshot(Guid id, Image screenshot)
    {
        var result = Repository.AddScreenshot(id, screenshot);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public UpdateResult<Success> RemoveScreenshot(Guid id, Image screenshot)
    {
        var result = Repository.RemoveScreenshot(id, screenshot);

        if (result is false)
            return new Failed();

        return new Success();
    }

	public int Count()
	{
		return Repository.Count();
	}

	public int CountByName(string name)
	{
		return Repository.CountByName(name);
	}

	public int CountByGenre(string genre)
	{
		return Repository.CountByGenre(genre);
	}

	public int CountByGenres(List<string> genres)
	{
		return Repository.CountByGenres(genres);
	}
}