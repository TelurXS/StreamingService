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

        builder.HasMany(x => x.Genres)
            .WithMany(x => x.Titles);

        builder.HasMany(x => x.Series)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.Rates)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.LocalizedNames)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.LocalizedDescriptions)
            .WithOne(x => x.Title);
    }
}