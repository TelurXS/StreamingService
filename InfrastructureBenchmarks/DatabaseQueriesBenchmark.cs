using BenchmarkDotNet.Attributes;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureBenchmarks;

[MemoryDiagnoser]
public class DatabaseQueriesBenchmark
{
    private static DataContext _dataContext;
    
    [GlobalSetup]
    public static void GlobalSetup()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DataContext>(x =>
        {
            x.UseSqlServer("");
        });

        var provider = services.BuildServiceProvider();

        _dataContext = provider.GetRequiredService<DataContext>();
    }
    
    [Benchmark]
    public static void DefaultQuery()
    {
        
    }
}