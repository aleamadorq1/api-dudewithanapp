using System.ComponentModel.DataAnnotations;

namespace DudeWithAnApi.Models
{
    public class QuoteTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuoteId { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        public string PrimaryText { get; set; }

        [Required]
        public string SecondaryText { get; set; }

        public int? IsDeleted { get; set; }
    }

}

