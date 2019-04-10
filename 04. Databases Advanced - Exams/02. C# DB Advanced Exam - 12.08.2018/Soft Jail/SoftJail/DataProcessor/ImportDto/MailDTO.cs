﻿namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class MailDTO
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Za-z ]+ str.$")]
        public string Address { get; set; }
    }
}
