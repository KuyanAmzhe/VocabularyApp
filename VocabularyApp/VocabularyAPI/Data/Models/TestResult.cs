using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyAPI.Data.Models
{
    public enum TestTypes
    {
        EngToRus,
        RusToEng
    };
    [Table("TestsResults")]
    public class TestResult
    {
        public int Id { get; set; }
        public TestTypes TestType { get; set; }
        public DateTime TestDate { get; set; }
        [MaxLength(50)]
        public string? TestedPersonName { get; set; }
        public int TotalQuestionsNumber { get; set; }
        public int WrongAnswersNumber { get; set; }
        public string? WrongAnsweredQuestions { get; set; }
    }
}
