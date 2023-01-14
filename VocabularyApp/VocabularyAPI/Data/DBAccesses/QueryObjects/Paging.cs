namespace VocabularyAPI.Data.DBAccesses.QueryObjects
{
    public static class Paging
    {
        /// <summary>
        /// Returns the <c>pageSize</c> of query depending
        /// on the <c>pageNum</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IQueryable<T> SetPage<T>(
            this IQueryable<T> query,
            int pageSize, int pageNum)
        {
            pageNum--;

            //If it is the first page
            if (pageSize == 0)
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize), pageSize, "page size can't be zero");

            //If somehow page is less than 0
            if (pageNum < 0)
                return query.Take(pageSize);

            //If it is not the first page and everything is fine
            if (pageNum != 0)
                query = query.Skip(pageSize * pageNum);

            return query.Take(pageSize);
        }
    }
}
