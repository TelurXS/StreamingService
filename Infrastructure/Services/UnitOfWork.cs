using Domain.Interfaces.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(DataContext dataContext)
    {
		DataContext = dataContext;
	}

	private DataContext DataContext { get; }

	public void SaveChages()
	{
		DataContext.SaveChanges();
	}
}
