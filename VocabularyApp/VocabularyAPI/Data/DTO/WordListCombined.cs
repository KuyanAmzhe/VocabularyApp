using VocabularyAPI.Data.DBAccesses;
using VocabularyAPI.Data.Models;

namespace VocabularyAPI.Data.DTO
{
    public class WordListCombined
    {
        public WordListCombined(
            SortFilterPageOptions sortFilterPageData,
            IEnumerable<WordDictionary> wordsList,
            IEnumerable<string> partsOfSpeach)
        {
            SortFilterPageData = sortFilterPageData;
            WordsList = wordsList;
            PartsOfSpeach = partsOfSpeach;
        }
        public SortFilterPageOptions SortFilterPageData { get; set; }
        public IEnumerable<WordDictionary> WordsList { get; set; }
        public IEnumerable<string> PartsOfSpeach { get; set; }
    }
}
