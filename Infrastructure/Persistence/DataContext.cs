using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<LocalizedName> LocalizedNames { get; set; }
    public DbSet<LocalizedDescription> LocalizedDescriptions { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Title> Titles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = AssemblyReference.Assembly;
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}