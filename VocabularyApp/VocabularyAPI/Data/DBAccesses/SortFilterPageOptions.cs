namespace VocabularyAPI.Data.DBAccesses
{
    public class SortFilterPageOptions
    {
        public enum SortQueryOptions
        {
            None = 0,
            Ascending,
            Descending
        }

        public enum FilterQueryOptions
        {
            None = 0,
            ByPartsOfSpeach
        }

        public int NumPages { get; set; }

        public const int PageSize = 20;
        public int PageNumber { get; set; }
        public string PrevCheckState { get; set; }

        public SortQueryOptions SortQueryOption { get; set; }
        public FilterQueryOptions FilterQueryOption { get; set; }
        public string FilterValue { get; set; }
        public int FilterByState { get; set; }

        /// <summary>
        /// Sets up number of pages and makes sure page number
        /// in the right range
        /// </summary>
        public void SetupPages<T> (IQueryable<T> query)
        {
            //Sets up number of pages
            NumPages = (int) Math.Ceiling(
                (double) query.Count() / PageSize);
            //Mages sure PageNumber in right range
            this.PageNumber = Math.Min(
                Math.Max(1, PageNumber), NumPages);

            string newCheckState = GenerateCheckState();
            //If filter, sort options are changed goes to the first page with new options
            if (PrevCheckState != newCheckState)
                PageNumber = 1;

            PrevCheckState = newCheckState;
        }

        private string GenerateCheckState()
        {
            return $"{(int)SortQueryOption},{(int)FilterQueryOption},{FilterValue},";
        }

        /// <summary>
        /// Sets sort, filter options and filter value out of string
        /// PrevCheckState property
        /// </summary>
        /// <param name="checkState"></param>
        public void SetOptionsFromCheckState(
            string checkState)
        {
            //In general, here is just a string parsing
            this.SortQueryOption = (SortQueryOptions)int.Parse(
                checkState.Substring(0, checkState.IndexOf(',')));

            checkState = checkState.Substring(checkState.IndexOf(',') + 1);

            this.FilterQueryOption = (FilterQueryOptions)int.Parse(
                checkState.Substring(0, checkState.IndexOf(',')));

            checkState = checkState.Substring(checkState.IndexOf(',') + 1);

            this.FilterValue = checkState.Substring(0, checkState.IndexOf(','));
        }
    }
}
