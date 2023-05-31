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
            var posts = _postService.GetPosts();
            var postsMaterialized = _mapper.Map<IEnumerable<PostDTO>>(posts);
            var response = new ApiResponse<IEnumerable<PostDTO>> (postsMaterialized);
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetPost([FromQuery] int id)
        {
            var post = await _postService.GetPostById(id);
            var postMaterialized = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postMaterialized);
            return Ok(response);
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO newPost )
        {
            var newPostEntity = _mapper.Map<Post>(newPost);
            var postResponse = await _postService.AddPost(newPostEntity);
            var entityToDTO = _mapper.Map<PostDTO>(postResponse);
            var response = new ApiResponse<PostDTO>(entityToDTO);
            return Ok(response);
        }

        [HttpPut("update-post")]
        public async Task<IActionResult> UpdatePost([FromBody] PostDTO post)
        {
            var dtoToEntity = _mapper.Map<Post>(post);
            var postResponse = await _postService.UpdatePost(dtoToEntity);
            var response = new ApiResponse<bool>(postResponse);
            return Ok(response);
        }

        [HttpDelete("delete-post")]
        public async Task<IActionResult> DeletePost([FromQuery] int id)
        {
            var postResponse = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(postResponse);
            return Ok(response);
        }
    }
}
