namespace Cinema.DataProcessor.ExportDto
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class CustomerDto
    {
        [XmlAttribute]
        public string FirstName { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }

        [XmlElement]
        public string SpentMoney { get; set; }

        [XmlElement]
        public string SpentTime { get; set; }
    }
}
