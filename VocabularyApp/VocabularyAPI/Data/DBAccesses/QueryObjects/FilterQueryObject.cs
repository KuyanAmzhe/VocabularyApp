using static VocabularyAPI.Data.DBAccesses.SortFilterPageOptions;
using VocabularyAPI.Data.Models;
using System.Linq.Dynamic.Core;

namespace VocabularyAPI.Data.DBAccesses.QueryObjects
{
    public static class FilterQueryObject
    {
        /*public static IQueryable<WordDictionary> FilterBy(
            this IQueryable<WordDictionary> query,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            if (string.IsNullOrEmpty(filterColumn) && string.IsNullOrEmpty(filterQuery))
                return query;

            query = query.Where(
                string.Format("{0}.StartsWith(\"{1}\")", filterColumn, filterQuery));

            return query;
        }*/

        public static IQueryable<T> FilterBy<T>(
            this IQueryable<T> query,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            if (string.IsNullOrEmpty(filterColumn) && string.IsNullOrEmpty(filterQuery))
                return query;

            query = query.Where(
                string.Format("{0}.StartsWith(\"{1}\")", filterColumn, filterQuery));

            return query;
        }

        //OLD METHOD. CONSIDER DELETING
        public static IQueryable<WordDictionary> FilterQueryBy(
            this IQueryable<WordDictionary> query
            , FilterQueryOptions options
            , string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                return query;

            switch (options)
            {
                case FilterQueryOptions.None:
                    return query;
                case FilterQueryOptions.ByPartsOfSpeach:
                    return query.Where(x => x.PartOfSpeech == filterValue);

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(options), options, null);
            }
        }
    }
}
