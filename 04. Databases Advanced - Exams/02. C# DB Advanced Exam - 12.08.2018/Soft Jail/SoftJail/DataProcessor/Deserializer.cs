namespace SoftJail.DataProcessor
{

    using Data;
    using Data.Models;
    using ImportDto;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore.Migrations.Operations;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            DepartmentCellDTO[] departmentCellDtos = JsonConvert.DeserializeObject<DepartmentCellDTO[]>(jsonString);

            List<Department> departments = new List<Department>();

            foreach (var dto in departmentCellDtos)
            {
                if (!IsValid(dto) || !dto.Cells.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                List<Cell> cells = new List<Cell>();

                foreach (var cell in dto.Cells)
                {
                    Cell currentCell = new Cell()
                    {
                        CellNumber = cell.CellNumber,
                        HasWindow = cell.HasWindow
                    };

                    cells.Add(currentCell);
                }

                Department department = new Department()
                {
                    Name = dto.Name,
                    Cells = cells
                };

                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            PrisonerMailDTO[] departmentCellDtos = JsonConvert.DeserializeObject<PrisonerMailDTO[]>(jsonString);

            List<Prisoner> prisoners = new List<Prisoner>();

            foreach (var dto in departmentCellDtos)
            {
                Cell currentCell = context.Cells.Find(dto.CellId);

                if (!IsValid(dto) || !dto.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                List<Mail> mails = new List<Mail>();
                foreach (var mail in dto.Mails)
                {
                    Mail currentMail = new Mail()
                    {
                        Description = mail.Description,
                        Sender = mail.Sender,
                        Address = mail.Address
                    };

                    mails.Add(currentMail);
                }

                DateTime? releaseDate = null;

                if (dto.ReleaseDate != null)
                {
                    releaseDate = DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                Prisoner currentPrisoner = new Prisoner()
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    IncarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = releaseDate,
                    Bail = dto.Bail,
                    Cell = currentCell,
                    Mails = mails
                };

                prisoners.Add(currentPrisoner);

                sb.AppendLine($"Imported {currentPrisoner.FullName} {currentPrisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(OfficerPrisonerDTO[]), new XmlRootAttribute("Officers"));

            OfficerPrisonerDTO[] deserializedOfficerPrisonerDtos = (OfficerPrisonerDTO[])serializer.Deserialize(new StringReader(xmlString));

            List<Officer> officers = new List<Officer>();

            foreach (var dto in deserializedOfficerPrisonerDtos)
            {
                Department department = context.Departments.Find(dto.DepartmentId);

                Position position;
                bool isValidPositionParse = Enum.TryParse<Position>(dto.Position, false, out position);

                Weapon weapon;
                bool isValidWeaponParse = Enum.TryParse<Weapon>(dto.Weapon, false, out weapon);

                if (!IsValid(dto) || !isValidPositionParse || !isValidWeaponParse)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Officer currentOfficer = new Officer()
                {
                    FullName = dto.FullName,
                    Salary = dto.Salary,
                    Position = Enum.Parse<Position>(dto.Position),
                    Weapon = Enum.Parse<Weapon>(dto.Weapon),
                    Department = department
                };

                foreach (var prisoner in dto.Prisoners)
                {
                    Prisoner currentPrisoner = context.Prisoners.Find(prisoner.Id);

                    if (prisoner != null)
                    {
                        OfficerPrisoner officerPrisoner = new OfficerPrisoner()
                        {
                            Prisoner = currentPrisoner
                        };

                        currentOfficer.OfficerPrisoners.Add(officerPrisoner);
                    }

                }

                officers.Add(currentOfficer);

                sb.AppendLine($"Imported {currentOfficer.FullName} ({currentOfficer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}