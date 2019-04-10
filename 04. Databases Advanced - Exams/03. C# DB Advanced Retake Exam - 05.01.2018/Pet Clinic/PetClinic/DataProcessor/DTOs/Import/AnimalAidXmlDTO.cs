namespace PetClinic.DataProcessor.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class AnimalAidXmlDTO
    {
        [XmlElement]
        public string Name { get; set; }
    }
}
