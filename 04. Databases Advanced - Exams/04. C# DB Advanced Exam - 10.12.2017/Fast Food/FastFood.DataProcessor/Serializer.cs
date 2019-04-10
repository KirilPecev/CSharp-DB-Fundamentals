namespace FastFood.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Dto.Export;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var orders = context
                .Employees
                .Where(e => e.Name == employeeName)
                .Select(e => new
                {
                    Name = e.Name,
                    Orders = e.Orders
                        .Where(o => o.Type.ToString() == orderType)
                        .Select(o => new
                        {
                            Customer = o.Customer,
                            Items = o.OrderItems.Select(i => new
                            {
                                Name = i.Item.Name,
                                Price = i.Item.Price,
                                Quantity = i.Quantity
                            }),
                            TotalPrice = o.OrderItems.Sum(i => i.Item.Price * i.Quantity)
                        })
                        .OrderByDescending(o => o.TotalPrice)
                        .ThenByDescending(o => o.Items.Count()),
                    TotalMade = e.Orders.Sum(o => o.OrderItems.Sum(i => i.Item.Price * i.Quantity))
                })
                .FirstOrDefault();

            string result = JsonConvert.SerializeObject(orders, Formatting.Indented);
            return result;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCategoriesDto[]), new XmlRootAttribute("Categories"));

            string[] categoryNames = categoriesString.Split(",");

            var categories = context
                .Categories
                .Where(c => categoryNames.Contains(c.Name))
                .Select(c => new ExportCategoriesDto()
                {
                    Name = c.Name,
                    MostPopularItem = c.Items.Select(i => new ExportItemDto()
                    {
                        Name = i.Name,
                        TotalMade = context.OrderItems
                            .Where(oi => oi.Item.Name == i.Name)
                            .Sum(oi => oi.Item.Price * oi.Quantity),
                        TimesSold = context.OrderItems
                            .Where(oi => oi.Item.Name == i.Name)
                            .Sum(oi => oi.Quantity)
                    })
                        .OrderByDescending(i => i.TotalMade)
                        .ThenByDescending(i => i.TimesSold)
                        .FirstOrDefault()

                })
                .OrderByDescending(c => c.MostPopularItem.TotalMade)
                .ThenByDescending(c => c.MostPopularItem.TimesSold)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), categories, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}