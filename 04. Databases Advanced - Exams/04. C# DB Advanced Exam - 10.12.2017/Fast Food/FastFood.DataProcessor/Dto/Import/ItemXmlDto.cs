namespace FastFood.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Item")]
    public class ItemXmlDto
    {
        [XmlElement]
        [Required]
        public string Name { get; set; }

        [XmlElement]
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
