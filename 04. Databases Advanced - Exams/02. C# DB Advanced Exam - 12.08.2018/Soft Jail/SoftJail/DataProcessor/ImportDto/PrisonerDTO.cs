namespace SoftJail.DataProcessor.ImportDto
{
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class PrisonerDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
