using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data.Models;
using VocabularyAPI.Data.DBAccesses;
using VocabularyAPI.Data.DBAccesses.QueryObjects;
using VocabularyAPI.Data.DTO;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VocabularyAPI.Data.DBAccesses.VocabularyAccess
{
    public class VocabularyActionsAccess
        : IVocabularyActionsAccess
{
        private readonly VocabularyDBContext _context;

        public VocabularyActionsAccess(VocabularyDBContext context)
        {
            _context = context;
        }

        /*public async Task<VocabularyApiResult<WordDictionary>> GetSortedFilteredVocabularyAsync(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            var wordsQuery = _context.Vocabulary
                .AsNoTracking()
                .FilterBy(filterColumn, filterQuery)
                .SortBy(sortColumn, sortOrder)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);

            var data = await wordsQuery.ToListAsync();

            int count = await _context.Vocabulary.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new VocabularyApiResult<WordDictionary>(
                data,
                count,
                pageNumber,
                pageSize,
                totalPages,
                sortColumn,
                sortOrder);
        }*/

        public async Task<VocabularyApiResult<T>> GetSortedFilteredAsync<T>(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null) where T : class
        {
            var requestQuery = _context.Set<T>()
                .AsNoTracking()
                .FilterBy(filterColumn, filterQuery)
                .SortBy(sortColumn, sortOrder)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);

            var data = await requestQuery.ToListAsync();

            int count = await _context.Set<T>().CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new VocabularyApiResult<T>(
                data,
                count,
                pageNumber,
                pageSize,
                totalPages,
                sortColumn,
                sortOrder);
        }

        public async Task<int> AddNewWordAsync(WordDictionary word)
        {
            await _context.Vocabulary.AddAsync(word);
            await _context.SaveChangesAsync();
            var addedWord = _context.Vocabulary
                .AsNoTracking()
                .SingleOrDefault(w => w.Word == word.Word);

            if (addedWord != null) return addedWord.Id;

            return -1;
        }
        public async Task<List<string>> GetPartsOfSpeachAsync()
        {
            return await _context.Vocabulary
                .Select(x => x.PartOfSpeech)
                .Distinct()
                .ToListAsync();
        }

        public async Task<WordDictionary> GetWordByIdAsync(int id)
        {
            id = id != 0 ? id : 1;
            var word = await _context.Vocabulary
                .FindAsync(id);

            if (word == null)
                throw new Exception("Word not found");

            return word;
        }

        public async Task UpdateWordAsync(WordDictionary word)
        {
            if (word.Id == 0) return;
            var wordToUpdate = await _context.Vocabulary
                .FindAsync(word.Id);

            if (wordToUpdate == null)
                throw new Exception("Word not found");

            wordToUpdate.Word = word.Word;
            wordToUpdate.PartOfSpeech = word.PartOfSpeech;
            wordToUpdate.Translation = word.Translation;
            wordToUpdate.Description = word.Description;

            await _context.SaveChangesAsync();
        }
        public async Task<List<WordDictionary>> GetWordsAsync(int numberOfWords)
        {
            if (numberOfWords == null)
                return null;

            var maxIdWord = await _context.Vocabulary
                .AsNoTracking()
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            var rand = new Random();
            //list of UNIQUE words that would be returned
            var list = new List<WordDictionary>();

            //loop that fills the list
            for (int i = 0; i < numberOfWords; i++)
            {
                //gets random word in 1 to maxIdWord + 1 range
                var word = await _context.Vocabulary
                    .FindAsync(rand.Next(1, maxIdWord.Id + 1));

                //if a word is not in DB, then continue the loop
                if (word == null)
                {
                    i--;
                    continue;
                }

                //adds a word to the list if it's not there
                if (!list.Contains(word))
                    list.Add(word);
                else
                    i--;
            }

            return list;
        }

        public async Task<int> WordCountAsync()
        {
            return await _context.Vocabulary
                .CountAsync();
        }

        public async Task AddTestResultAsync(TestResult testResult)
        {
            await _context.TestsResults.AddAsync(testResult);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWordAsync(int wordId)
        {
            //Since I use not actual deleting from the DB
            //then I just change the SoftDeleted field
            //of the element to be deleted
            var toBeDeletedWord = await _context.Vocabulary.FindAsync(wordId);

            if (toBeDeletedWord == null) return;

            toBeDeletedWord.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
