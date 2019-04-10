namespace SoftJail.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Data.Models.Enums;

    [XmlType("Officer")]
    public class OfficerPrisonerDTO
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string FullName { get; set; }

        [XmlElement("Money")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Salary { get; set; }

        [XmlElement]
        [Required]
        public string Position { get; set; }

        [XmlElement]
        [Required]
        public string Weapon { get; set; }

        [XmlElement]
        [Required]
        public int DepartmentId { get; set; }

        [XmlArray]
        public PrisonerDTO[] Prisoners { get; set; }
    }
}
