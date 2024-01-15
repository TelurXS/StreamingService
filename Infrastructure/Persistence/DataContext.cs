using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class DataContext : IdentityDbContext<User, Role, Guid>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Name> Names { get; set; }
    public DbSet<Description> Descriptions { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = AssemblyReference.Assembly;
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}