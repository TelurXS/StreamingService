using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TitleConfiguration: IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Slug)
            .IsUnique();
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1024)
            .IsRequired();

        builder.HasOne(x => x.Image);

        builder.HasMany(x => x.Screenshots);

        builder.HasMany(x => x.Series);
        
        builder.HasMany(x => x.LocalizedNames);
        
        builder.HasMany(x => x.LocalizedDescriptions);
    }
}