namespace Repositories.Basic
{
    public class PaginationResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        //public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int TotalPages { get; set; }
        public PaginationResult()
        {

        }

        public PaginationResult(IEnumerable<T> items, int currentPage, int pageSize, int totalItems, int totalPage)
        {
            Items = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalPage;
        }
    }
}
