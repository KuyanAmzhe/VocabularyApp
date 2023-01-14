using Newtonsoft.Json;
using VocabularyAPI.Data.Models;
using VocabularyAPI.Extenstions.Adapters;
using VocabularyAPI.Extenstions.POCOs;

namespace VocabularyAPI.Extenstions
{
    public static class DictionaryJSONLoader
    {
        /// <summary>
        /// Loads initial data for seeding from JSON-file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<WordDictionary> LoadDictionary(string filePath)
        {
            var wordsJSONList = JsonConvert.DeserializeObject<ICollection<WordJSON>>(File.ReadAllText(filePath));

            List<WordDictionary> wordsList = new List<WordDictionary>();

            if (wordsJSONList == null)
            {
                wordsList.Add(
                    new WordDictionary
                    {
                        Word = "Error",
                        PartOfSpeech = "Noun",
                        Translation = "Ошибка"
                    });
                return wordsList;
            }

            foreach (var wordJSON in wordsJSONList)
            {
                wordsList.Add(WordDictionaryAdapter.AdaptWordJSON(wordJSON));
            }

            return wordsList;
        }
    }
}
