using VocabularyAPI.Data.Models;
using VocabularyAPI.Data.DTO;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;

namespace VocabularyAPI.Data.DBAccesses.VocabularyAccess
{
    public interface IVocabularyActionsAccess
    {
        /// <summary>
        /// Returns sorted, filtered query according given T class
        /// </summary>
        /// <param name="pageNumber">
        /// Number of page
        /// </param>
        /// <param name="pageSize">
        /// Size of the page
        /// </param>
        /// <param name="T">
        /// class of the table that you want to access
        /// </param>>
        /// <returns></returns>
        public Task<VocabularyApiResult<T>> GetSortedFilteredAsync<T>(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null) where T : class;
        /// <summary>
        /// Adds new test result to DB
        /// </summary>
        /// <param name="testResult">
        /// test result that will be added
        /// </param>
        /// <returns></returns>
        public Task AddTestResultAsync(TestResult testResult);

        //-------------------------OLD METHODS. CONSIDER DELETING
        //-------------------------UPDATE: NOT ALL OF THEM ARE OLD. I MESSED UP

        /// <summary>
        /// Returns all parts of speach which database contains
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetPartsOfSpeachAsync();
        /// <summary>
        /// Deletes a word using given id
        /// </summary>
        /// <param name="wordId"></param>
        public Task DeleteWordAsync(int wordId);
        /// <summary>
        /// Returns the number of words in DB
        /// </summary>
        /// <returns></returns>
        public Task<int> WordCountAsync();
        /// <summary>
        /// Adds new word to dictionary
        /// </summary>
        public Task<int> AddNewWordAsync(WordDictionary word);
        /// <summary>
        /// Returns a word by id
        /// </summary>
        /// <param name="id">
        /// Word id
        /// </param>
        /// <returns></returns>
        public Task<WordDictionary> GetWordByIdAsync(int id);
        /// <summary>
        /// Updates word depending on id that given object has
        /// </summary>
        /// <param name="word">
        /// Word that you want update
        /// </param>
        /// <returns></returns>
        public Task UpdateWordAsync(WordDictionary word);
        /// <summary>
        /// Returns a list of the desired number of words
        /// </summary>
        /// <param name="numberOfWords">
        /// the desired number of words
        /// </param>
        /// <param name="totalQuestionNumber">
        /// total number of question that would be n
        /// </param>
        /// <returns>List of words, or null if numberOfWords is null</returns>
        public Task<List<WordDictionary>> GetWordsAsync(int numberOfWords);
    }
}