namespace PetClinic.DataProcessor.DTOs.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Vet")]
    public class VetDTO
    {
        [XmlElement]
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Name { get; set; }

        [XmlElement]
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Profession { get; set; }

        [XmlElement]
        [Required]
        [Range(22, 65)]
        public int Age { get; set; }

        [XmlElement]
        [Required]
        [RegularExpression("^((\\+359)[0-9]{9})|(0[0-9]{9})$")]
        public string PhoneNumber { get; set; }
    }
}
