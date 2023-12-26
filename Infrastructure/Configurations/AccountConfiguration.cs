using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Login)
            .IsUnique();
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Login)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasMany(x => x.Rates)
            .WithOne(x => x.Author);
    }
}