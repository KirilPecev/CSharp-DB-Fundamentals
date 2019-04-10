namespace FastFood.DataProcessor.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class ExportCategoriesDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public ExportItemDto MostPopularItem { get; set; }
    }
}
