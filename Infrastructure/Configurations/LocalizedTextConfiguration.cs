using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class LocalizedTextConfiguration : IEntityTypeConfiguration<LocalizedText>
{
    public void Configure(EntityTypeBuilder<LocalizedText> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Language)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(1024)
            .IsRequired();
    }
}