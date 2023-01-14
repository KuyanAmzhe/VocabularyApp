using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.IdentityModel.Tokens;
using VocabularyAPI.Data.Models;
using static VocabularyAPI.Data.DBAccesses.SortFilterPageOptions;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace VocabularyAPI.Data.DBAccesses.QueryObjects
{
    public static class SortQueryObject
    {
        /*public static IQueryable<WordDictionary> SortBy(
            this IQueryable<WordDictionary> query,
            string? sortColumn = null,
            string? sortOrder = null)
        {
            if (string.IsNullOrEmpty(sortColumn) || !IsValidProperty(sortColumn)) return query;

            sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC"
                ? "ASC"
                : "DESC";

            query = query.OrderBy(string.Format("{0} {1}", sortColumn, sortOrder));

            return query;
        }*/

        public static IQueryable<T> SortBy<T>(
            this IQueryable<T> query,
            string? sortColumn = null,
            string? sortOrder = null)
        {
            if (string.IsNullOrEmpty(sortColumn) || !IsValidProperty<T>(sortColumn)) return query;

            sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC"
                ? "ASC"
                : "DESC";

            query = query.OrderBy(string.Format("{0} {1}", sortColumn, sortOrder));

            return query;
        }

        private static bool IsValidProperty<T>(
            string propertyName)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public|
                BindingFlags.Instance);

            return prop != null;
        }

        //OLD METHODS. CONSIDER DELETING
        public static IQueryable<WordDictionary> SortQueryBy(
            this IQueryable<WordDictionary> query,
            SortQueryOptions options)
        {
            switch(options)
            {
                case SortQueryOptions.None:
                    return query;
                case SortQueryOptions.Ascending:
                    return query.OrderBy(x => x.Word);
                case SortQueryOptions.Descending:
                    return query.OrderByDescending(x => x.Word);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(options), options, null);
            }
        }
    }
}
