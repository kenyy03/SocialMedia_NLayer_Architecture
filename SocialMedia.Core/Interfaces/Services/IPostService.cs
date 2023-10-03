using SocialMedia.Core.DTOs;
using SocialMedia.Core.DTOs.PostDTOs;
using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<Post> GetPostById(int id);
        PagedList<PostDTO> GetPosts(PostRequest request);
        Task<Post> AddPost(Post post);
        Task<bool> DeletePost(int id);
        Task<bool> UpdatePost(Post postToUpdate);
    }
}
