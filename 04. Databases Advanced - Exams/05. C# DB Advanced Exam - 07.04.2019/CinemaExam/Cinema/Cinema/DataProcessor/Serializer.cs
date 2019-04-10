namespace Cinema.DataProcessor
{
    using Data;
    using ExportDto;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(m => m.Rating >= rating && m.Projections.Any(t => t.Tickets.Any()))
                .OrderByDescending(c => c.Rating)
                .ThenByDescending(m => m.Projections.Sum(x => x.Tickets.Sum(t => t.Price)))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("F2"),
                    TotalIncomes = m.Projections.Sum(x => x.Tickets.Sum(t => t.Price)).ToString("F2"),
                    Customers = context.Tickets.Where(t => t.Projection.Movie.Id == m.Id).Select(t => new
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = t.Customer.Balance.ToString("F2")
                    })
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                })
                .Take(10);

            string result = JsonConvert.SerializeObject(movies, Formatting.Indented);
            return result;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));

            var customers = context
                .Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(t => t.Price))
                .Select(c => new CustomerDto()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = $"{c.Tickets.Sum(t => t.Price):F2}",
                    SpentTime = TimeSpan.FromTicks(c.Tickets.Sum(t => t.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss")
                })
                .Take(10)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[]
            {
                 XmlQualifiedName.Empty,
            });

            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}