using SocialMedia.Core.Entities.PostEntity;
using SocialMedia.Core.Entities.UserEntity;

namespace SocialMedia.Core.Entities.CommentEntity
{
    public class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }

        public virtual Post? IdPostNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
