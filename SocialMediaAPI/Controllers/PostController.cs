using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Response;
using SocialMedia.Core.DTOs.PostDTOs;
using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postRepository, IMapper mapper)
        {
            if(postRepository == null) throw new ArgumentNullException(nameof(postRepository));
            if(mapper == null) throw new ArgumentNullException(nameof(mapper));
            _postService = postRepository;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public IActionResult GetPosts()
        {
            try
            {
                var posts = _postService.GetPosts();
                var postsMaterialized = _mapper.Map<IEnumerable<PostDTO>>(posts);
                var response = ApiResponse<IEnumerable<PostDTO>>.Success(postsMaterialized);
                return Ok(response);
            }
            catch (Exception ex)
            {
                IEnumerable<PostDTO> postDTOs = Enumerable.Empty<PostDTO>();
                var response = ApiResponse<IEnumerable<PostDTO>>.Failure(postDTOs, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetPost([FromQuery] int id)
        {
            try
            {
                var post = await _postService.GetPostById(id);
                var postMaterialized = _mapper.Map<PostDTO>(post);
                var response = ApiResponse<PostDTO>.Success(postMaterialized);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<PostDTO>.Failure(new PostDTO(), ex.InnerException?.Message ?? ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO newPost )
        {
            try
            {
                var newPostEntity = _mapper.Map<Post>(newPost);
                var postResponse = await _postService.AddPost(newPostEntity);
                var entityToDTO = _mapper.Map<PostDTO>(postResponse);
                var response = ApiResponse<PostDTO>.Success(entityToDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<PostDTO>.Failure(new PostDTO(), ex.InnerException?.Message ?? ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPut("update-post")]
        public async Task<IActionResult> UpdatePost([FromBody] PostDTO post)
        {
            try
            {
                var dtoToEntity = _mapper.Map<Post>(post);
                var postResponse = await _postService.UpdatePost(dtoToEntity);
                var response = ApiResponse<bool>.Success(postResponse);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<bool>.Failure(false, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("delete-post")]
        public async Task<IActionResult> DeletePost([FromQuery] int id)
        {
            try
            {
                var postResponse = await _postService.DeletePost(id);
                var response = ApiResponse<bool>.Success(postResponse);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<bool>.Failure(false, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(response);
            }
        }
    }
}
