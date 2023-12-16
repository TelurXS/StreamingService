using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class DataContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(IAssemblyMark).Assembly;
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}