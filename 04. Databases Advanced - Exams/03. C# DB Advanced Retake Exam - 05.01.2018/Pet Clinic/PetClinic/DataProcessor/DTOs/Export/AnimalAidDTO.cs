namespace PetClinic.DataProcessor.DTOs.Export
{
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class AnimalAidDTO
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public decimal Price { get; set; }
    }
}
