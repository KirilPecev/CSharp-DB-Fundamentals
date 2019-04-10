namespace PetClinic.DataProcessor.DTOs.Import
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class PassportDTO
    {
        [Key]
        [RegularExpression("^[A-Za-z]{7}[0-9]{3}$")]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        [Required]
        [RegularExpression("^((\\+359)[0-9]{9})|(0[0-9]{9})$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}
