using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public const int NAME_MAX_LENGTH = 128;
	public const int FIRSTNAME_MAX_LENGTH = 128;
	public const int SECONDNAME_MAX_LENGTH = 128;

	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd();

		builder.Property(x => x.Name)
			.HasMaxLength(NAME_MAX_LENGTH);

		builder.Property(x => x.FirstName)
			.HasMaxLength(FIRSTNAME_MAX_LENGTH);
		
		builder.Property(x => x.SecondName)
			.HasMaxLength(SECONDNAME_MAX_LENGTH);

		builder.Property(x => x.BirthDate);

		builder.Property(x => x.SubscriptionExpiresIn);

		builder.HasOne(x => x.Subscription);

		builder.HasMany(x => x.Rates)
			.WithOne(x => x.Author);

		builder.HasMany(x => x.Comments)
			.WithOne(x => x.Author);

		builder.HasMany(x => x.FavouriteGenres)
			.WithMany(x => x.FavouriteInUsers);

		builder.HasMany(x => x.FavouriteTitles)
			.WithMany(x => x.FavouriteInUsers);

		builder.HasMany(x => x.ViewRecords)
			.WithOne(x => x.Author);

		builder.HasMany(x => x.Followers)
			.WithMany()
			.UsingEntity<Dictionary<string, object>>(
			nameof(User.Followers),
			e => e.HasOne<User>().WithMany().HasForeignKey("UserId"),
			e => e.HasOne<User>().WithMany().HasForeignKey("FollowerId"));

		builder.HasMany(x => x.Readers)
			.WithMany()
			.UsingEntity<Dictionary<string, object>>(
			nameof(User.Readers),
			e => e.HasOne<User>().WithMany().HasForeignKey("ReaderId"),
			e => e.HasOne<User>().WithMany().HasForeignKey("UserId"));

		builder.HasMany(x => x.Lists)
			.WithOne(x => x.Author);
	}
}
