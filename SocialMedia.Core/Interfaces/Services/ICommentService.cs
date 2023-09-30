using SocialMedia.Core.DTOs;
using SocialMedia.Core.DTOs.CommentDTOs;

namespace SocialMedia.Core.Interfaces.Services
{
    public interface ICommentService
    {
        PagedList<CommentDTO> GetComments(CommentRequest request);
    }
}
