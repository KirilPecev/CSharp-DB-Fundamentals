namespace FastFood.DataProcessor
{
    using Data;
    using Dto.Import;
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
    using Models.Enums;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportEmployeeDto[] deserializedEmployees = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);

            List<Employee> employees = new List<Employee>();
            List<Position> positions = new List<Position>();

            foreach (var dto in deserializedEmployees)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Position position = positions.FirstOrDefault(p => p.Name == dto.Position) ?? new Position()
                {
                    Name = dto.Position
                };

                Employee employee = new Employee()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Position = position
                };

                positions.Add(position);
                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessMessage, employee.Name));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportItemDto[] deserializedItems = JsonConvert.DeserializeObject<ImportItemDto[]>(jsonString);

            List<Item> items = new List<Item>();
            List<Category> categories = new List<Category>();

            foreach (var dto in deserializedItems)
            {
                Item item = items.FirstOrDefault(i => i.Name == dto.Name);

                if (!IsValid(dto) || item != null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Category category = categories.FirstOrDefault(c => c.Name == dto.Category) ?? new Category()
                {
                    Name = dto.Category
                };

                item = new Item()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Category = category
                };

                categories.Add(category);
                items.Add(item);
                sb.AppendLine(String.Format(SuccessMessage, item.Name));
            }

            context.Items.AddRange(items);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportOrdersDto[]), new XmlRootAttribute("Orders"));

            ImportOrdersDto[] deserializedOrders = (ImportOrdersDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Order> orders = new List<Order>();

            foreach (var dto in deserializedOrders)
            {
                Employee employee = context.Employees.FirstOrDefault(e => e.Name == dto.Employee);

                bool isValidType = Enum.TryParse(dto.Type, out OrderType type);

                if (!IsValid(dto) || !dto.Items.All(IsValid) || employee == null || !isValidType)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Order order = new Order()
                {
                    Employee = employee,
                    Customer = dto.Customer,
                    DateTime = DateTime.ParseExact(dto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = type
                };

                bool hasInvalidItem = false;

                foreach (var itemDto in dto.Items)
                {
                    Item item = context.Items.FirstOrDefault(i => i.Name == itemDto.Name);
                    if (item == null)
                    {
                        sb.AppendLine(FailureMessage);
                        hasInvalidItem = true;
                        break;
                    }

                    order.OrderItems.Add(new OrderItem()
                    {
                        Item = item,
                        Quantity = itemDto.Quantity
                    });

                }

                if (!hasInvalidItem)
                {
                    orders.Add(order);
                    sb.AppendLine($"Order for {order.Customer} on {order.DateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)} added");
                }
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();

            bool result = Validator.TryValidateObject(obj, context, results, true);

            return result;
        }
    }
}