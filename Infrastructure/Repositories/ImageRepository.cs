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

    public List<Image> FindAll()
    {
        return Entities
            .AsNoTracking()
            .ToList();
    }

    public Image Insert(Image value)
    {
        return Entities.Add(value).Entity;
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
}