namespace VocabularyAPI.Data.DTO
{
    public class VocabularyApiResult<T>
    {
        public VocabularyApiResult(
            List<T> finalQuery,
            int count,
            int pageNumber,
            int pageSize,
            int totalPages,
            string? sortColumn,
            string? sortOrder)
        {
            Data = finalQuery;
            TotalPages = totalPages;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }
        public List<T> Data { get; private set; } 
        public int TotalCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public string? SortColumn { get; private set; }
        public string? SortOrder { get; private set; }
    }
}
