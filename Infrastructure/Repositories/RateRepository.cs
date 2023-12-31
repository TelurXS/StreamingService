﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RateRepository : EntityRepository<Rate>, IRateRepository
{
    public RateRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Rate? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Rate> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
            .ToList();
    }

    public Rate Insert(Rate value)
    {
        return Entities.Add(value).Entity;
    }

    public bool Update(Guid id, Rate value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Value, x => value.Value));

        return result > 0;
    }

    public bool Delete(Rate value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }

    public List<Rate> FindAllByAuthor(Account account)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
            .Where(x => x.Author.Id == account.Id)
            .ToList();
    }

    public List<Rate> FindAllByTitle(Title title)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
            .Where(x => x.Title.Id == title.Id)
            .ToList();
    }
}