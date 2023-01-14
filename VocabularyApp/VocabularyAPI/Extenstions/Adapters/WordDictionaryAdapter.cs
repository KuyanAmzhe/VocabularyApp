using VocabularyAPI.Data.DTO;
using VocabularyAPI.Data.Models;
using VocabularyAPI.Extenstions.POCOs;

namespace VocabularyAPI.Extenstions.Adapters
{
    public static class WordDictionaryAdapter
    {
        /// <summary>
        /// Adapts WordJSON object to WordDictionary object
        /// </summary>
        /// <param name="wordJSON"></param>
        /// <returns></returns>
        public static WordDictionary AdaptWordJSON(WordJSON wordJSON)
        {
            return new WordDictionary
            {
                Word = wordJSON.Word,
                PartOfSpeech = wordJSON.PartOfSpeech,
                Translation = wordJSON.Translation
            };
        }
        /// <summary>
        /// Adapts AddWordModel object to WordDictionary object
        /// </summary>
        /// <param name="newWord"></param>
        /// <returns></returns>
        public static WordDictionary AdaptAddWordModel(AddWordModel newWord)
        {
            return new WordDictionary
            {
                Word = newWord.Word,
                PartOfSpeech = newWord.PartOfSpeach,
                Translation = newWord.Translation,
                Description = newWord.Description
            };
        }
    }
}
