using SocialMedia.Core.Entities.CommentEntity;
using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Core.Entities.UserEntity
{
    public class User : BaseEntity
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            Names = string.Empty;
            LastNames = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
