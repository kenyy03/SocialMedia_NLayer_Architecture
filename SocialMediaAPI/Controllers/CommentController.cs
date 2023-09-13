using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Response;
using SocialMedia.Core.DTOs.CommentDTOs;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get-all")]
        public IActionResult GetPosts()
        {
            try
            {
                var comments = _commentService.GetComments();
                var response = ApiResponse<List<CommentDTO>>.Success(comments);
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
