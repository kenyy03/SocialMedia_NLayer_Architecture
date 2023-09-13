using SocialMedia.Core.DTOs.CommentDTOs;

namespace SocialMedia.Core.Interfaces.Services
{
    public interface ICommentService
    {
        List<CommentDTO> GetComments();
    }
}
