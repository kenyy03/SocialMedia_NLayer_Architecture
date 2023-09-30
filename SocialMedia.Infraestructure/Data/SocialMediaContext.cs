using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities.CommentEntity;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Entities.UserEntity;
using SocialMedia.Infraestructure.Config.CommentMap;
using SocialMedia.Infraestructure.Config.PostMap;
using SocialMedia.Infraestructure.Config.UserMap;

namespace SocialMedia.Infraestructure.Data
{
    public class SocialMediaContext : DbContext
    {
        public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserMap(modelBuilder.Entity<User>());
            new PostMap(modelBuilder.Entity<Post>());
            new CommentMap(modelBuilder.Entity<Comment>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
