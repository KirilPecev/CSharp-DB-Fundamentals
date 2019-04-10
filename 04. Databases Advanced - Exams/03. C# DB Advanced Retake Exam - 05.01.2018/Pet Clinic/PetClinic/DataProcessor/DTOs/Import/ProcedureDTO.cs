namespace PetClinic.DataProcessor.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureDTO
    {
        [XmlElement]
        public string Vet { get; set; }

        [XmlElement]
        public string Animal { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidXmlDTO[] AnimalAids { get; set; }
    }
}
