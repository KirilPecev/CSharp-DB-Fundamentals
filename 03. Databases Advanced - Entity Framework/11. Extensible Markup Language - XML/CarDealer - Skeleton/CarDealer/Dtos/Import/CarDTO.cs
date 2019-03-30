namespace CarDealer.Dtos.Import
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class CarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public int TravelledDistance { get; set; }

        [XmlArray("parts")]
        public HashSet<CarPartDTO> Parts { get; set; }
    }

    [XmlType("partId")]
    public class CarPartDTO
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
