using SocialMedia.Core.DTOs;
using SocialMedia.Core.DTOs.CommentDTOs;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Interfaces.Services;
using SocialMedia.Core.Enumerations;

namespace SocialMedia.Core.Services.SocialMediaServices
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWorkFactory unitOfWorkFactory) 
        {
            _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(UnitOfWorkType.SocialMedia);
        }

        public PagedList<CommentDTO> GetComments(CommentRequest request)
        {
            DateTime startDate = request?.StartDate ?? DateTime.MinValue;
            DateTime endDate = request?.EndDate ?? DateTime.MaxValue;
            int page = request?.Page ?? 1;
            int pageSize = request?.Pagesize ?? 10;
            string spName = "EXEC SpGetActiveComments @StartDate='{0}', @EndDate='{1}'";
            var query = string.Format(spName, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            IQueryable<CommentDTO> comments = _unitOfWork.RawSqlQuery(query, map => new CommentDTO
            {
                CommentId = Convert.ToInt32(map["CommentId"] ?? 0),
                PostId = Convert.ToInt32(map["PostId"] ?? 0),
                UserId = Convert.ToInt32(map["UserId"] ?? 0),
                PostDescription = Convert.ToString(map["PostDescription"]),
                UserFullName = Convert.ToString(map["UserFullName"]),
                CommentDescription = Convert.ToString(map["CommentDescription"]),
                CommentDate = Convert.ToDateTime(map["CommentDate"]),
            });

            return PagedList<CommentDTO>.Create(comments, page, pageSize);
        }
    }
}
