namespace SocialMedia.Core.DTOs.CommentDTOs
{
    public class CommentRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;  }
    }
}
