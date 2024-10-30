using ImageSharing.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageSharing.Auth.Infra.EF.Mapping;

public class UserMap: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserName).HasMaxLength(50).HasColumnName("username").IsRequired();
        builder.Property(x => x.Email).HasMaxLength(50).HasColumnName("email").IsRequired();
        builder.Property(x => x.AvatarPath).HasMaxLength(255).HasColumnName("avatar").IsRequired();
        builder.Property(x => x.HashedPassword).HasMaxLength(255).HasColumnName("password").IsRequired();
        builder.Property(x => x.Base64Salt).HasMaxLength(255).HasColumnName("salt").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("is_active").IsRequired();
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();
    }
}