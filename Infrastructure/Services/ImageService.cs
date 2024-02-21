using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class ImageService : IImageService
{
    public ImageService(IImageRepository repository)
    {
        Repository = repository;
    }
    
    private IImageRepository Repository { get; }
    
    public GetResult<Image> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

	public GetResult<Image> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Image> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
	}

	public GetAllResult<Image> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public CreateResult<Image> Create(Image value)
    {
		var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Image> Update(Guid id, Image value)
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

	public DeleteResult Delete(Image value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

	public int Count()
	{
		return Repository.Count();
	}
}