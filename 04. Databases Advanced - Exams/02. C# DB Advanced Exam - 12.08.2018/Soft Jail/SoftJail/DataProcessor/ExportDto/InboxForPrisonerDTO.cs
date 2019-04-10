namespace SoftJail.DataProcessor.ExportDto
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class InboxForPrisonerDTO
    {
        [XmlElement]
        public int Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public MessageDTO[] Messages { get; set; }
    }
}
