using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<Post> GetPostById(int id);
        IEnumerable<Post> GetPosts();
        Task<Post> AddPost(Post post);
        Task<bool> DeletePost(int id);
        Task<bool> UpdatePost(Post postToUpdate);
    }
}
