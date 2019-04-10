namespace PetClinic.DataProcessor
{
    using Data;
    using DTOs.Export;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context
                .Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(a => new
                {
                    OwnerName = a.Passport.OwnerName,
                    AnimalName = a.Name,
                    Age = a.Age,
                    SerialNumber = a.Passport.SerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToArray();

            var result = JsonConvert.SerializeObject(animals, Formatting.Indented);

            return result;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProcedureDTO[]), new XmlRootAttribute("Procedures"));

            var procedures = context
                .Procedures
                .OrderBy(p => p.DateTime)
                .ThenBy(p => p.Animal.Passport.SerialNumber)
                .Select(p => new ProcedureDTO()
                {
                    Passport = p.Animal.Passport.SerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids.Select(a => new AnimalAidDTO()
                    {
                        Name = a.AnimalAid.Name,
                        Price = a.AnimalAid.Price
                    })
                        .ToArray(),
                    TotalPrice = p.ProcedureAnimalAids.Sum(a => a.AnimalAid.Price)
                })
                .ToArray();

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), procedures, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}
