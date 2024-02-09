using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ViewRecordConfiguration : IEntityTypeConfiguration<ViewRecord>
{
	public void Configure(EntityTypeBuilder<ViewRecord> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd();

		builder.Property(x => x.Progress)
			.IsRequired();

		builder.HasOne(x => x.Author)
			.WithMany(x => x.ViewRecords);

		builder.HasOne(x => x.Series);

		builder.HasOne(x => x.Title)
			.WithMany(x => x.ViewRecords)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
