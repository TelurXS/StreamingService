using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TitlesListConfiguration : IEntityTypeConfiguration<TitlesList>
{
	public const int NAME_MAX_LENGTH = 64;

	public void Configure(EntityTypeBuilder<TitlesList> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd();

		builder.Property(x => x.Availability)
			.IsRequired();

		builder.Property(x => x.Name)
			.HasMaxLength(NAME_MAX_LENGTH)
			.IsRequired();

		builder.HasOne(x => x.Author)
			.WithMany(x => x.Lists);

		builder.HasMany(x => x.Titles)
			.WithMany(x => x.Lists);
	}
}
