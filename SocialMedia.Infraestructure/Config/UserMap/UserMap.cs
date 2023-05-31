using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities.UserEntity;

namespace SocialMedia.Infraestructure.Config.UserMap
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder) 
        {
            entityBuilder.ToTable("User");
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Id).HasColumnName("UserId");
            entityBuilder.Property(e => e.Names).HasColumnName("Names").HasMaxLength(50).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.LastNames).HasColumnName("LastNames").HasMaxLength(50).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.Email).HasColumnName("Email").HasMaxLength(30).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.BirthDate).HasColumnName("BirthDate").HasColumnType("datetime");
            entityBuilder.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(10).IsRequired().IsUnicode(false);
            entityBuilder.Property(e => e.Active).HasColumnName("Active").IsUnicode(false);
            entityBuilder.HasMany(e => e.Posts).WithOne(e => e.IdUserNavigation).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            entityBuilder.HasMany(e => e.Comments).WithOne(e => e.IdUserNavigation).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
