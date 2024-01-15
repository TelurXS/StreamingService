using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public const int CONTENT_MAX_LENGTH = 512;

    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Content)
            .HasMaxLength(CONTENT_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.CreationDate)
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Comments);

        builder.HasOne(x => x.Title)
            .WithMany(x => x.Comments);

        builder.HasMany(x => x.Childs)
            .WithOne(x => x.Parent);
    }
}
