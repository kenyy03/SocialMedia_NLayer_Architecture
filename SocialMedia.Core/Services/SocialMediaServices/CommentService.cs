using SocialMedia.Core.DTOs.CommentDTOs;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Interfaces.Services;

namespace SocialMedia.Core.Services.SocialMediaServices
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public List<CommentDTO> GetComments()
        {
            string query = "EXEC SpGetActiveComments";
            var comments = _unitOfWork.RawSqlQuery<CommentDTO>(query, map => new CommentDTO
            {
                CommentId = Convert.ToInt32(map["CommentId"] ?? 0),
                PostId = Convert.ToInt32(map["PostId"] ?? 0),
                UserId = Convert.ToInt32(map["UserId"] ?? 0),
                PostDescription = Convert.ToString(map["PostDescription"]),
                UserFullName = Convert.ToString(map["UserFullName"]),
                CommentDescription = Convert.ToString(map["CommentDescription"]),
                CommentDate = Convert.ToDateTime(map["CommentDate"]),
            });

            return comments;
        }
    }
}
