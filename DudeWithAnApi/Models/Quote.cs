﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DudeWithAnApi.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuoteText { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
