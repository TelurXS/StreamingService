using BenchmarkDotNet.Attributes;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureBenchmarks;

[MemoryDiagnoser]
public class DatabaseQueriesBenchmark
{
    private static DataContext _dataContext;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        var builder = new DbContextOptionsBuilder()
            .UseSqlServer("Server=DESKTOP-43HN7TU;Database=StreamingServiceDb;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");
        
        _dataContext = new DataContext(builder.Options);

        _dataContext.Database.EnsureCreated();
    }
    
    [Benchmark]
    public Account? DefaultQuery()
    {
        return _dataContext
            .Accounts
            .FirstOrDefault(x => x.Id == Guid.NewGuid());
    }

    [Benchmark]
    public Account? SplitQuery()
    {
		return _dataContext
			 .Accounts
             .AsSplitQuery()
			 .FirstOrDefault(x => x.Id == Guid.NewGuid());
	}
}