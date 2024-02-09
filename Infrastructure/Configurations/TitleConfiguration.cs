using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TitleConfiguration: IEntityTypeConfiguration<Title>
{
    public const int NAME_MAX_LENGTH = 128;
    public const int SLUG_MAX_LENGTH = 128;
    public const int DESCRIPTION_MAX_LENGTH = 1024;
    public const int DIRECTOR_MAX_LENGTH = 64;
    public const int CAST_MAX_LENGTH = 512;
    
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

        builder.Property(x => x.Country)
            .IsRequired();

        builder.Property(x => x.AgeRestriction)
            .IsRequired();

        builder.Property(x => x.AvarageRate)
            .IsRequired();

        builder.Property(x => x.Director)
            .HasMaxLength(DIRECTOR_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Cast)
            .HasMaxLength(CAST_MAX_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.Image);

        builder.HasMany(x => x.Screenshots);

        builder.HasMany(x => x.Genres)
            .WithMany(x => x.Titles);

        builder.HasMany(x => x.Series)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.Rates)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.Names)
            .WithOne(x => x.Title);
        
        builder.HasMany(x => x.Descriptions)
            .WithOne(x => x.Title);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Title);

        builder.HasMany(x => x.Lists)
            .WithMany(x => x.Titles);

        builder.HasMany(x => x.FavouriteInUsers)
            .WithMany(x => x.FavouriteTitles);

		builder.HasMany(x => x.ViewRecords)
			.WithOne(x => x.Title);
	}
}