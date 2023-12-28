using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class LocalizedDescriptionConfiguration : IEntityTypeConfiguration<LocalizedDescription>
{
    public const int LANGUAGE_MAX_LENGTH = 16;
    public const int VALUE_MAX_LENGTH = 1024;
    
    public void Configure(EntityTypeBuilder<LocalizedDescription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Language)
            .HasMaxLength(LANGUAGE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(VALUE_MAX_LENGTH)
            .IsRequired();
    }
}