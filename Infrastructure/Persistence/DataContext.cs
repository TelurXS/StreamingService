using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class DataContext : DbContext
{
    public required DbSet<Account> Accounts { get; set; }
    public required DbSet<LocalizedText> LocalizedTexts { get; set; }
    public required DbSet<Image> Images { get; set; }
    public required DbSet<Series> Series { get; set; }
    public required DbSet<Title> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = AssemblyReference.Assembly;
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}