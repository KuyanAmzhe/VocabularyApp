namespace VocabularyAPI.Extenstions.POCOs
{
    public class WordJSON
    {
        public string PartOfSpeech { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }

        public WordJSON(string partOfSpeach,
            string word,
            string translation)
        {
            PartOfSpeech = partOfSpeach;
            Word = word;
            Translation = translation;
        }
    }
}
