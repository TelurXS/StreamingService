using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public const int NAME_MAX_LENGTH = 64;
    
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(NAME_MAX_LENGTH)
            .IsRequired();

        builder.HasMany(x => x.Titles)
            .WithMany(x => x.Genres);

		builder.HasMany(x => x.FavouriteInUsers)
			.WithMany(x => x.FavouriteGenres);
	}
}