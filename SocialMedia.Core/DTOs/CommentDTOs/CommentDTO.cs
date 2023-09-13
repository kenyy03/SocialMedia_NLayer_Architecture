namespace SocialMedia.Core.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string PostDescription { get; set; }
        public string UserFullName { get; set; }
        public string CommentDescription { get; set; }
        public DateTime CommentDate { get; set; }

        public CommentDTO()
        {
            CommentId = 0;
            PostId = 0;
            UserId = 0;
            PostDescription = string.Empty;
            UserFullName = string.Empty;
            CommentDescription = string.Empty;
            CommentDate = DateTime.MinValue;
        }
    }
}
