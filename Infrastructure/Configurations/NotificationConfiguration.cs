using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public const int MESSAGE_MAX_LENGTH = 256;
	public const int LOCALIZABLE_MESSAGE_MAX_LENGTH = 128;
	public const int LINK_MAX_LENGTH = 256;

	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
		   .ValueGeneratedOnAdd();

		builder.Property(x => x.Message)
			.HasMaxLength(MESSAGE_MAX_LENGTH)
			.IsRequired();

		builder.Property(x => x.LocalizabledMessage)
			.HasMaxLength(LOCALIZABLE_MESSAGE_MAX_LENGTH)
			.IsRequired();

		builder.Property(x => x.Link)
			.HasMaxLength(LINK_MAX_LENGTH);

		builder.Property(x => x.Snoozed)
			.IsRequired();

		builder.Property(x => x.Date)
			.IsRequired();

		builder.HasOne(x => x.Receiver)
			.WithMany(x => x.Notifications);

		builder.HasOne(x => x.RelatedUser);
	}
}
