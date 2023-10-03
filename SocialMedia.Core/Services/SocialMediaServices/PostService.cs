using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.DTOs.PostDTOs;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Entities.UserEntity;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.Core.Services.SocialMediaServices
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        public PostService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(UnitOfWorkType.SocialMedia);
            _postRepository = _unitOfWork.GetRepository<Post>();
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _postRepository.FindByIdAsync(id);
            return post ?? new Post();
        }

        public PagedList<PostDTO> GetPosts(PostRequest request)
        {
            int page = request?.Page ?? 1;
            int pageSize = request?.Pagesize ?? 10;
            var posts = _postRepository.AsQueryable()
                                       .AsNoTracking()
                                       .Select(s => s.ToDto())
                                       .AsQueryable();
            return PagedList<PostDTO>.Create(posts, page, pageSize);
        }

        public async Task<Post> AddPost(Post post)
        {
            //var user = await _unitOfWork.Users.FindByIdWithCollections(post.UserId, new string[] {"Comments", "Posts"} );
            _unitOfWork.BeginTransaction();
            Post postAdded = new ();
            try
            {
                var user = _userRepository.AsQueryable()
                                          .AsNoTracking()
                                          .Include(c => c.Comments)
                                          .Include(c => c.Posts)
                                          .FirstOrDefault(f => f.Id == post.UserId);
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
                postAdded = await _postRepository.AddAsync(post);
                await _unitOfWork.SaveAsync();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
            }
            return postAdded;
        }

        public async Task<bool> DeletePost(int id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                await _postRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                return false;
            }
        }

        public async Task<bool> UpdatePost(Post postToUpdate)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _postRepository.Update(postToUpdate);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                return false;
            }
        }
    }
}
