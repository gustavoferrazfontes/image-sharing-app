using ImageSharing.Posts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ImageSharing.Posts.Infra.EF.Mapping;

internal class PostMap: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.Subtitle).HasColumnName("subtitle").HasMaxLength(200);
        builder.Property(x => x.Tags).HasColumnName("tags").HasMaxLength(255);
        builder.Property(x => x.ImagePath).HasColumnName("image_path");
    }
}