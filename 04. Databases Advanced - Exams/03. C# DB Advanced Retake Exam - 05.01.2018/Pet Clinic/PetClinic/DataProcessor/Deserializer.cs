namespace PetClinic.DataProcessor
{
    using Data;
    using DTOs.Import;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            AnimalAidDTO[] deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidDTO[]>(jsonString);

            List<AnimalAid> animalAids = new List<AnimalAid>();

            foreach (var dto in deserializedAnimalAids)
            {
                var animalAid = animalAids.FirstOrDefault(a => a.Name == dto.Name);

                if (!IsValid(dto) || animalAid != null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                animalAid = new AnimalAid()
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                animalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            AnimalDTO[] deserializedAnimals = JsonConvert.DeserializeObject<AnimalDTO[]>(jsonString);

            List<Passport> passports = new List<Passport>();
            List<Animal> animals = new List<Animal>();

            foreach (var dto in deserializedAnimals)
            {
                var passport = passports.FirstOrDefault(p => p.SerialNumber == dto.Passport.SerialNumber);

                if (!IsValid(dto) || !IsValid(dto.Passport) || passport != null)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                passport = Mapper.Map<Passport>(dto.Passport);
                passports.Add(passport);

                Animal animal = new Animal()
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Age = dto.Age,
                    Passport = passport
                };

                animals.Add(animal);
                sb.AppendLine($"Record {animal.Name} Passport №: {passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(VetDTO[]), new XmlRootAttribute("Vets"));

            VetDTO[] deserializedVets = (VetDTO[])serializer.Deserialize(new StringReader(xmlString));

            List<Vet> vets = new List<Vet>();

            foreach (var dto in deserializedVets)
            {
                Vet vet = vets.FirstOrDefault(v => v.PhoneNumber == dto.PhoneNumber);

                if (!IsValid(dto) || vet != null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                vet = new Vet()
                {
                    Name = dto.Name,
                    Profession = dto.Profession,
                    Age = dto.Age,
                    PhoneNumber = dto.PhoneNumber
                };

                vets.Add(vet);

                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(ProcedureDTO[]), new XmlRootAttribute("Procedures"));

            ProcedureDTO[] deserializedProcedures = (ProcedureDTO[])serializer.Deserialize(new StringReader(xmlString));

            List<Procedure> procedures = new List<Procedure>();

            foreach (var dto in deserializedProcedures)
            {
                Vet vet = context.Vets.FirstOrDefault(v => v.Name == dto.Vet);
                Animal animal = context.Animals.FirstOrDefault(a => a.PassportSerialNumber == dto.Animal);

                if (vet == null || animal == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                List<AnimalAid> currentAids = new List<AnimalAid>();

                Procedure procedure = new Procedure()
                {
                    DateTime = DateTime.ParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Vet = vet,
                    Animal = animal
                };

                bool hasInvalidData = false;
                foreach (var aidDto in dto.AnimalAids)
                {
                    AnimalAid aid = context.AnimalAids.FirstOrDefault(a => a.Name == aidDto.Name);

                    if (aid == null || currentAids.FirstOrDefault(a => a.Name == aid.Name) != null)
                    {
                        sb.AppendLine("Error: Invalid data.");
                        hasInvalidData = true;
                        break;
                    }

                    procedure.ProcedureAnimalAids.Add(new ProcedureAnimalAid()
                    {
                        AnimalAid = aid
                    });

                    currentAids.Add(aid);
                }

                if (!hasInvalidData)
                {
                    procedures.Add(procedure);

                    sb.AppendLine("Record successfully imported.");
                }
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            bool result = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return result;
        }
    }
}
