using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class ImageRepository : EntityRepository<Image>, IImageRepository
{
    public ImageRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Image? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
    }

	public Image? FindByIdWithTracking(Guid id)
	{
		return Entities
			.FirstOrDefault(x => x.Id == id);
	}

	public List<Image> FindAll(int count = 10, int page = 0)
    {
        return Entities
            .AsNoTracking()
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Image> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public Image? Insert(Image value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

    public bool Update(Guid id, Image value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Uri, x => value.Uri));

        return result > 0;
    }

    public bool Delete(Image value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }

	public int Count()
	{
		return Entities.Count();
	}
}