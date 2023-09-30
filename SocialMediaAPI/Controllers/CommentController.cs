using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.DTOs.CommentDTOs;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
        }

        [HttpGet("get-comments-by-dates")]
        public async Task<IActionResult> GetPosts([FromQuery] CommentRequest request)
        {
            try
            {
                var comments = _commentService.GetComments(request);
                var response = ApiResponse<PagedList<CommentDTO>>.Success(comments);
                return Ok(response);
            }
            catch (Exception ex)
            {
                List<CommentDTO> commentDTOs = new();
                var response = ApiResponse<List<CommentDTO>>.Failure(commentDTOs, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(response);
            }
        }
    }
}
