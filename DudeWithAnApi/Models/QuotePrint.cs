using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DudeWithAnApi.Models
{
    public class QuotePrint
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Quote")]
        public int QuoteId { get; set; }
        public DateTime PrintedAt { get; set; }
        public string RequestId { get; set; }

        public virtual Quote Quote { get; set; }
    }
}
