using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.Data.DBAccesses;
using VocabularyAPI.Data.DBAccesses.VocabularyAccess;
using VocabularyAPI.Extenstions.Adapters;
using VocabularyAPI.Data.DTO;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data.Models;
using VocabularyAPI.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace VocabularyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly IVocabularyActionsAccess _service;
        public VocabularyController(
            IVocabularyActionsAccess service)
        {
            _service = service;
        }

        // GET: api/Vocabulary
        [HttpGet]
        public async Task<ActionResult<VocabularyApiResult<WordDictionary>>> Vocabulary(
            int pageNumber = 0,
            int pageSize = 10,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            return await _service.GetSortedFilteredAsync<WordDictionary>(
                pageNumber,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
        }

        [HttpGet]
        public async Task<ActionResult<VocabularyApiResult<TestResult>>> TestsResults(
            int pageNumber = 0,
            int pageSize = 10,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            return await _service.GetSortedFilteredAsync<TestResult>(
                pageNumber,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetPartsOfSpeech()
        {
            return await _service.GetPartsOfSpeachAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddNewWord(WordDictionary newWord)
        {
            return await _service.AddNewWordAsync(newWord);
        }

        [HttpGet]
        public async Task<ActionResult<WordDictionary>> GetWordById(int id)
        {
            return await _service.GetWordByIdAsync(id);
        }

        [HttpPut]
        public async Task UpdateWord(WordDictionary word)
        {
            await _service.UpdateWordAsync(word);
        }

        [HttpDelete]
        public async Task DeleteWord(int id)
        {
            await _service.DeleteWordAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<WordDictionary>>> GetWordsForTest(
            int numberOfWords,
            int totalQuestionsInTest)
        {
            //if the number of words in DB less than totalQuestionsInTest, then it means
            //there are not enough words, so empty list
            return (await _service.WordCountAsync() < totalQuestionsInTest)
                ? new List<WordDictionary>()
                : await _service.GetWordsAsync(numberOfWords);
        }

        [HttpGet]
        public async Task<int> GetWordsCount()
        {
            return await _service.WordCountAsync();
        }

        [HttpPost]
        public async Task AddTestResult(TestResult testResult)
        {
            await _service.AddTestResultAsync(testResult);
        }
    }
}
