using SocialMedia.Core.DTOs;

namespace SocialMedia.Core.Extensions
{
    public static class PaginatedListExtensions
    {

        public static PaginatedList<T> AsPaginatedList<T>(this IQueryable<T> items, int page, int pageSize) where T : class
        {
            return PaginatedList<T>.Create(items, page, pageSize);
        }
    }
}
