using SocialMedia.Core.Entities.CommentEntity;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Entities.UserEntity;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> Posts { get; }
        IRepository<User> Users { get; }
        IRepository<Comment> Comments { get; }
        void Commit();
        Task CommitAsync();
    }
}
