namespace SocialMedia.Core.DTOs
{
    public abstract class BaseRequest
    {
        public int Page { get; set; }
        public int Pagesize { get; set; }
    }
}
