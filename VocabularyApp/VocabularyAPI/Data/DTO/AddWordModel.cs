using System.ComponentModel.DataAnnotations;

namespace VocabularyAPI.Data.DTO
{
    public class AddWordModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Min length is {1}")]
        [MaxLength(25, ErrorMessage = "Max length is {1}")]
        public string Word { get; set; }
        [Required]
        public string PartOfSpeach { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Min length is {1}")]
        [MaxLength(30, ErrorMessage = "Max length is {1}")]
        public string Translation { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length is {1}")]
        public string? Description { get; set; }
        public IEnumerable<string>? PartsOfSpeach { get; set; }
    }
}
