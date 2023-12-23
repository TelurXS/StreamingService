using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; } = null!;
    public DbSet<LocalizedText> LocalizedTexts { get; } = null!;
    public DbSet<Image> Images { get; } = null!;
    public DbSet<Series> Series { get; } = null!;
    public DbSet<Title> Titles { get; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = AssemblyReference.Assembly;
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}