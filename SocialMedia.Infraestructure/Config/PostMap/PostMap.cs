using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Infraestructure.Config.PostMap
{
    public class PostMap
    {
        public PostMap(EntityTypeBuilder<Post> entityBuilder)
        {
            entityBuilder.ToTable("Post");
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Id).HasColumnName("PostId");
            entityBuilder.Property(e => e.Description).HasColumnName("Description").HasMaxLength(1000).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.Image).HasColumnName("Image").HasMaxLength(500).IsRequired(false).IsUnicode(false);
            entityBuilder.Property(e => e.Date).HasColumnName("Date").HasColumnType("datetime");
            entityBuilder.HasOne(e => e.IdUserNavigation).WithMany(t => t.Posts).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
