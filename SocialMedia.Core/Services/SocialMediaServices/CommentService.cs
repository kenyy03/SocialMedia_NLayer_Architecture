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
            DateTime startDate = new(2011, 1, 1);
            DateTime endDate = new(2018, 12, 12);
            string spName = "EXEC SpGetActiveComments @StartDate='{0}', @EndDate='{1}'";
            string query = string.Format(spName, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            
            var comments = _unitOfWork.RawSqlQuery(query, map => new CommentDTO
            {
                CommentId = Convert.ToInt32(map["CommentId"] ?? 0),
                PostId = Convert.ToInt32(map["PostId"] ?? 0),
                UserId = Convert.ToInt32(map["UserId"] ?? 0),
                PostDescription = Convert.ToString(map["PostDescription"]) ?? string.Empty,
                UserFullName = Convert.ToString(map["UserFullName"]) ?? string.Empty,
                CommentDescription = Convert.ToString(map["CommentDescription"]) ?? string.Empty,
                CommentDate = Convert.ToDateTime(map["CommentDate"]),
            });

            return comments;
        }
    }
}
