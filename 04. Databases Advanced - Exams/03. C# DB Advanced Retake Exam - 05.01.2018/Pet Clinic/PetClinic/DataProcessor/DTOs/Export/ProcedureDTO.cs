namespace PetClinic.DataProcessor.DTOs.Export
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureDTO
    {
        [XmlElement]
        public string Passport { get; set; }

        [XmlElement]
        public string OwnerNumber { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray]
        public AnimalAidDTO[] AnimalAids { get; set; }

        [XmlElement]
        public decimal TotalPrice { get; set; }
    }
}
