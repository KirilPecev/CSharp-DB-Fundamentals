namespace Cinema.DataProcessor.ImportDto
{
    using System.Xml.Serialization;

    [XmlType("Projection")]
    public class ProjectionDto
    {
        [XmlElement]
        public int MovieId { get; set; }

        [XmlElement]
        public int HallId { get; set; }

        [XmlElement]
        public string DateTime { get; set; }
    }
}
