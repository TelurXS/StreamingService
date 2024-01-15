using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public const int NAME_MAX_LENGTH = 64;
    public const int LOGIN_MAX_LENGTH = 32;
    public const int EMAIL_MAX_LENGTH = 64;
    public const int PASSWORD_MAX_LENGTH = 256;
    
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Login)
            .IsUnique();
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Login)
            .HasMaxLength(LOGIN_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(EMAIL_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(PASSWORD_MAX_LENGTH)
            .IsRequired();
    }
}