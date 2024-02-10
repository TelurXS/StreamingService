using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    public const int NAME_MAX_LENGTH = 128;
    public const int URI_MAX_LENGTH = 256;
    public const int DUBBING_MAX_LENGTH = 128;
    
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(NAME_MAX_LENGTH)
            .IsRequired();
        
        builder.Property(x => x.Dubbing)
            .HasMaxLength(NAME_MAX_LENGTH)
            .IsRequired();
        
        builder.Property(x => x.Uri)
            .HasMaxLength(URI_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Index)
            .IsRequired();

        builder.HasOne(x => x.Title)
            .WithMany(x => x.Series);
    }
}