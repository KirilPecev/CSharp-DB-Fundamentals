namespace SoftJail.DataProcessor
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
    using AutoMapper.QueryableExtensions;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                 .Prisoners
                 .Where(p => ids.Contains(p.Id))
                 .Select(p => new
                 {
                     p.Id,
                     Name = p.FullName,
                     CellNumber = p.Cell.CellNumber,
                     Officers = p.PrisonerOfficers.Select(o => new
                     {
                         OfficerName = o.Officer.FullName,
                         Department = o.Officer.Department.Name
                     })
                         .OrderBy(o => o.OfficerName),
                     TotalOfficerSalary = Math.Round(p.PrisonerOfficers.Sum(o => o.Officer.Salary), 2)
                 })
                 .OrderBy(p => p.Name)
                 .ThenBy(p => p.Id)
                 .ToArray();

            string result = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] names = prisonersNames.Split(",");

            var prisoners = context
                .Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(p => new InboxForPrisonerDTO
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    Messages = p.Mails.Select(m => new MessageDTO()
                    {
                        Description = GetEncrypted(m.Description)
                    })
                        .ToArray()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();

            //Using automapper
            //var prisoners = context
            //    .Prisoners
            //    .Where(p => names.Contains(p.FullName))
            //    .ProjectTo<InboxForPrisonerDTO>()
            //    .ToList();

            //foreach (var dto in prisoners)
            //{
            //    foreach (var message in dto.Messages)
            //    {
            //        message.Description = string.Join("", message.Description.Reverse());
            //    }
            //}

            XmlSerializer serializer = new XmlSerializer(typeof(InboxForPrisonerDTO[]), new XmlRootAttribute("Prisoners"));

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                XmlQualifiedName.Empty,
            });

            serializer.Serialize(new StringWriter(sb), prisoners, namespaces);

            return sb.ToString().TrimEnd();
        }

        private static string GetEncrypted(string msg)
        {
            return string.Join("", msg.Reverse());
        }
    }
}