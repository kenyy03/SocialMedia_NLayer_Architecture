namespace SocialMedia.Core.DTOs
{
    public class PaginatedList<T>
    {
        private PaginatedList(IReadOnlyCollection<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IReadOnlyCollection<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static PaginatedList<T> Create(IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, page, pageSize, totalCount);
        }
    }
}
