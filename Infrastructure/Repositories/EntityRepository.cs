using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class EntityRepository<T> where T : class
{
    protected EntityRepository(DataContext dataContext)
    {
        Context = dataContext;
        Entities = Context.Set<T>();
    }
    
    protected DataContext Context { get; }
    protected DbSet<T> Entities { get; }
}