namespace SoftJail.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Message")]
    public class MessageDTO
    {
        [XmlElement]
        public string Description { get; set; }
    }
}
