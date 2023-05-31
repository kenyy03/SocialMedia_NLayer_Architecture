using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities.CommentEntity;

namespace SocialMedia.Infraestructure.Config.CommentMap
{
    public class CommentMap
    {
        public CommentMap(EntityTypeBuilder<Comment> entityBuilder)
        {
            entityBuilder.ToTable("Comment");
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Id).HasColumnName("CommentId");
            entityBuilder.Property(e => e.Description).HasColumnName("Description").HasMaxLength(500).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.Date).HasColumnName("Date").HasColumnType("datetime");
            entityBuilder.Property(e => e.Active).HasColumnName("Active").IsUnicode(false);
            entityBuilder.HasOne(e => e.IdUserNavigation).WithMany(t => t.Comments).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            entityBuilder.HasOne(e => e.IdPostNavigation).WithMany(t => t.Comments).HasForeignKey(f => f.PostId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
