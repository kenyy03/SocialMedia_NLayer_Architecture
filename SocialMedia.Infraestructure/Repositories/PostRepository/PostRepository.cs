using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Interfaces.IPost;
using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts() 
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id) 
        {
            var postFound = await _context.Posts.FirstOrDefaultAsync(f => f.Id == id);
            return postFound ?? new Post();
        }

        public async Task<Post> CreatePost(Post newPost)
        {
            var post = _context.Posts.Add(newPost).Entity;
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var postFound = await GetPost(post.Id);
            if (postFound == null) return false;
            postFound.Description = post.Description;
            postFound.Date = post.Date;
            postFound.Image = post.Image;

            int rowAffected = await _context.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<bool> DeletePost(int id)
        {
            var postFound = await GetPost(id);
            if (postFound == null) return false;
            _context.Posts.Remove(postFound);
            int rowAffected = await _context.SaveChangesAsync();
            return rowAffected > 0;
        }
    }
}
