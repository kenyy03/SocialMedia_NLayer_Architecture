using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.Core.Services.SocialMediaServices
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new BusinessExceptions(nameof(unitOfWork));
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _unitOfWork.Posts.FindById(id);
            return post;
        }

        public IEnumerable<Post> GetPosts()
        {
            var posts = _unitOfWork.Posts.GetAll();
            return posts;
        }

        public async Task<Post> AddPost(Post post)
        {
            var user = await _unitOfWork.Users.FindByIdWithCollections(post.UserId, new string[] {"Comments", "Posts"} );
            if (user == null) throw new BusinessExceptions($"User: {nameof(user)} doesn't exists");
            if (post.Description.Contains("Sexo")) throw new BusinessExceptions("No Content Allowed");
            var userPosts = user.Posts;
            if (userPosts.Count < 10)
            {
                var lastPost = userPosts.OrderByDescending(o => o.Date).FirstOrDefault();
                if (lastPost == null) throw new BusinessExceptions($"User doesn't have posts or last post is invalid: {nameof(lastPost)}");
                bool isPostWithLessSevenDays = (DateTime.Now - lastPost.Date).TotalDays < 7;
                if (isPostWithLessSevenDays) throw new BusinessExceptions("You aren't able to publish posts");
            }
            var postAdded = await _unitOfWork.Posts.Add(post);
            await _unitOfWork.CommitAsync();
            return postAdded;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.Posts.Delete(id);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdatePost(Post postToUpdate)
        {
            _unitOfWork.Posts.Update(postToUpdate);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
