using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyAPI.Data.Models
{
    [Table("Vocabulary")]
    public class WordDictionary
    {
        public int Id { get; set; }
        public string PartOfSpeech { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string? Description { get; set; }
        //Sets true if the record is deleted
        public bool SoftDeleted { get; set; }
    }
}
