namespace PetClinic.DataProcessor.DTOs.Import
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AnimalAidDTO
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
