using SocialMedia.Core.Entities.CommentEntity;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Entities.UserEntity;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Repositories.GenericRepository;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private IRepository<Post> _postRepository;
        private IRepository<User> _userRepository;
        private IRepository<Comment> _commentRepository;
        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
        public IRepository<Post> Posts => _postRepository ??= new Repository<Post>(_context);
        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<Comment> Comments => _commentRepository ??= new Repository<Comment>(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
