namespace PetClinic.DataProcessor.DTOs.Import
{
    using System.ComponentModel.DataAnnotations;

    public class AnimalDTO
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public PassportDTO Passport { get; set; }

    }
}
