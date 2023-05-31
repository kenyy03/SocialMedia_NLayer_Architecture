using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Core.Interfaces.IPost
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post> CreatePost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
