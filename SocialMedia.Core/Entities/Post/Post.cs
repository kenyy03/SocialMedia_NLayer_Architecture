using SocialMedia.Core.DTOs.PostDTOs;
using SocialMedia.Core.Entities.CommentEntity;
using SocialMedia.Core.Entities.UserEntity;

namespace SocialMedia.Core.Entities.PostEntity
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Description = string.Empty;
            Image = string.Empty;
            IdUserNavigation = new();
        }

        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual User IdUserNavigation { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; }

        public PostDTO ToDto()
        {
            return new PostDTO
            {
                Date = Date,
                Description = Description,
                Id = Id,
                Image = Image,
                UserId = UserId
            };
        }
    }
}
