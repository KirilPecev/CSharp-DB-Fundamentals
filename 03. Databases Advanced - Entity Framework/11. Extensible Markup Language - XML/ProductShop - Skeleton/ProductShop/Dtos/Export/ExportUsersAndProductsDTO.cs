namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class ExportUsersAndProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUserDTO[] Users { get; set; }
    }
}
