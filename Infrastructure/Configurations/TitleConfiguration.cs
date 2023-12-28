using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TitleConfiguration: IEntityTypeConfiguration<Title>
{
    public const int NAME_MAX_LENGTH = 128;
    public const int SLUG_MAX_LENGTH = 128;
    public const int DESCRIPTION_MAX_LENGTH = 1024;
    
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Slug)
            .IsUnique();
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(SLUG_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(DESCRIPTION_MAX_LENGTH)
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