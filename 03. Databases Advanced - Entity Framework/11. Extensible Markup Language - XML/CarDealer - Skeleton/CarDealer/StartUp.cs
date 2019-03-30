namespace CarDealer
{
    using AutoMapper;
    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            string xmlFile = File.ReadAllText("../../../Datasets/sales.xml");
            string result;

            using (CarDealerContext context = new CarDealerContext())
            {
                //context.Database.EnsureCreated();
                result = GetSalesWithAppliedDiscount(context);
            }

            Console.WriteLine(result);
        }

        //09. Import Suppliers 
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SupplierDTO[]), new XmlRootAttribute("Suppliers"));

            SupplierDTO[] supplierDto = (SupplierDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Supplier> suppliers = new List<Supplier>();

            foreach (var dto in supplierDto)
            {
                Supplier user = Mapper.Map<Supplier>(dto);

                suppliers.Add(user);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {context.Suppliers.Count()}";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PartDTO[]), new XmlRootAttribute("Parts"));

            PartDTO[] partDto = (PartDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Part> parts = new List<Part>();

            foreach (var dto in partDto)
            {
                if (context.Suppliers.Find(dto.SupplierId) == null)
                {
                    continue;
                }

                Part part = Mapper.Map<Part>(dto);

                parts.Add(part);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {context.Parts.Count()}";
        }

        //11. Import Cars 
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CarDTO[]), new XmlRootAttribute("Cars"));

            CarDTO[] carDtos = (CarDTO[])serializer.Deserialize(new StringReader(inputXml));

            foreach (var car in carDtos)
            {
                Car currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };


                foreach (var part in car.Parts)
                {
                    bool isValid = currentCar.PartCars.FirstOrDefault(x => x.PartId == part.PartId) == null;
                    bool isPartValid = context.Parts.FirstOrDefault(p => p.Id == part.PartId) != null;

                    if (isValid && isPartValid)
                    {
                        currentCar.PartCars.Add(new PartCar()
                        {
                            PartId = part.PartId
                        });
                    }
                }

                context.Cars.Add(currentCar);
            }

            context.SaveChanges();

            return $"Successfully imported {context.Cars.Count()}";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerDTO[]), new XmlRootAttribute("Customers"));

            CustomerDTO[] customersDto = (CustomerDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Customer> customers = new List<Customer>();

            foreach (var dto in customersDto)
            {
                Customer customer = Mapper.Map<Customer>(dto);

                customers.Add(customer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SalesDTO[]), new XmlRootAttribute("Sales"));

            SalesDTO[] salesDtos = (SalesDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Sale> sales = new List<Sale>();

            foreach (var dto in salesDtos)
            {
                var car = context.Cars.Find(dto.CarId);
                var customer = context.Customers.Find(dto.CustomerId);

                if (car == null)
                {
                    continue;
                }

                Sale sale = Mapper.Map<Sale>(dto);

                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            Car[] cars = context
                .Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            ExportCarWithDistanceDTO[] exportCars = Mapper.Map<ExportCarWithDistanceDTO[]>(cars);


            string result = Serializer(typeof(ExportCarWithDistanceDTO[]), "cars", exportCars);

            return result;
        }

        //15. Export Cars From Make BMW 
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            Car[] cars = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            ExpotCarFromModelDTO[] exportedCars = Mapper.Map<ExpotCarFromModelDTO[]>(cars);

            string result = Serializer(typeof(ExpotCarFromModelDTO[]), "cars", exportedCars);

            return result;
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            Supplier[] suppliers = context
                .Suppliers
                .Include(x => x.Parts)
                .Where(s => s.IsImporter == false)
                .ToArray();

            ExportSupplierDTO[] exportSuppliers = Mapper.Map<ExportSupplierDTO[]>(suppliers);

            string result = Serializer(typeof(ExportSupplierDTO[]), "suppliers", exportSuppliers);

            return result;
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            ExportCarWithPartDTO[] cars = context
                .Cars
                .Select(c => new ExportCarWithPartDTO()
                {
                    Model = c.Model,
                    Make = c.Make,
                    TravelledDistance = c.TravelledDistance,
                    PartDto = c.PartCars.Select(p => new ExportPartDTO()
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            string result = Serializer(typeof(ExportCarWithPartDTO[]), "cars", cars);

            return result;
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            ExportSaleByCustomerDTO[] customers = context
                .Customers
                .Where(c => c.Sales.Any(s => s.Car != null))
                .Select(c => new ExportSaleByCustomerDTO()
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Select(x => x.Car).Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            string result = Serializer(typeof(ExportSaleByCustomerDTO[]), "customers", customers);
            return result;

        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            ExportSaleWithAppliedDiscountDTO[] sales = context
                .Sales
                .ProjectTo<ExportSaleWithAppliedDiscountDTO>()
                .ToArray();

            string result = Serializer(typeof(ExportSaleWithAppliedDiscountDTO[]), "sales", sales);
            return result;
        }

        public static string Serializer(Type type, string root, object obj)
        {
            XmlSerializer serializer = new XmlSerializer(type, new XmlRootAttribute(root));

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), obj, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}