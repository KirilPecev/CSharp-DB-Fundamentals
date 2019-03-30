namespace ProductShop
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

            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            string xmlFile = File.ReadAllText("../../../Datasets/categories-products.xml");
            string result;

            using (ProductShopContext context = new ProductShopContext())
            {
                //context.Database.EnsureCreated();
                result = GetUsersWithProducts(context);
            }

            Console.WriteLine(result);
        }

        //01. Import Users 
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserImportDTO[]), new XmlRootAttribute("Users"));

            UserImportDTO[] usersDto = (UserImportDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<User> users = new List<User>();

            foreach (var dto in usersDto)
            {
                User user = Mapper.Map<User>(dto);

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {context.Users.Count()}";
        }

        //02. Import Products 
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductImportDTO[]), new XmlRootAttribute("Products"));

            ProductImportDTO[] productsDto = (ProductImportDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Product> products = new List<Product>();

            foreach (var dto in productsDto)
            {
                Product product = Mapper.Map<Product>(dto);

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {context.Products.Count()}";
        }

        //03. Import Categories 
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryInputDTO[]), new XmlRootAttribute("Categories"));

            CategoryInputDTO[] categoriesDto = (CategoryInputDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<Category> categories = new List<Category>();

            foreach (var dto in categoriesDto)
            {
                if (dto.Name == null)
                {
                    continue;
                }

                Category product = Mapper.Map<Category>(dto);

                categories.Add(product);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {context.Categories.Count()}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryProductDTO[]), new XmlRootAttribute("CategoryProducts"));

            CategoryProductDTO[] categoriesProductsDto = (CategoryProductDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<CategoryProduct> categories = new List<CategoryProduct>();

            foreach (var dto in categoriesProductsDto)
            {
                bool hasCategory = context.Categories.Any(c => c.Id == dto.CategoryId);
                bool hasProduct = context.Products.Any(p => p.Id == dto.ProductId);

                if (hasCategory && hasProduct)
                {
                    CategoryProduct categoryProduct = Mapper.Map<CategoryProduct>(dto);

                    categories.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {context.CategoryProducts.Count()}";
        }

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            ProductExportInRangeDTO[] products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductExportInRangeDTO()
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerName = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();

            ProductExportInRangeDTO[] productExportDtos = Mapper.Map<ProductExportInRangeDTO[]>(products);

            XmlSerializer serializer = new XmlSerializer(typeof(ProductExportInRangeDTO[]), new XmlRootAttribute("Products"));

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                XmlQualifiedName.Empty
            });


            serializer.Serialize(new StringWriter(sb), productExportDtos, namespaces);

            return sb.ToString().TrimEnd();
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            User[] users = context
                .Users
                .Include(x => x.ProductsSold)
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)

                .ToArray();

            ExportUserSoldProductDTO[] exportSoldProductDto = Mapper.Map<ExportUserSoldProductDTO[]>(users);

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUserSoldProductDTO[]), new XmlRootAttribute("Users"));

            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                XmlQualifiedName.Empty
            });


            serializer.Serialize(new StringWriter(sb), exportSoldProductDto, namespaces);

            return sb.ToString().TrimEnd();
        }

        //07. Export Categories By Products Count 
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            ExportCategoryDTO[] categories = context.Categories
                .ProjectTo<ExportCategoryDTO>()
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            string result = Serializer(typeof(ExportCategoryDTO[]), "Categories", categories);

            return result;
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any())
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new ExportUserDTO()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new ExportSoldProductDTO()
                    {
                        Count = x.ProductsSold.Count,
                        Products = x.ProductsSold.Select(p => new ExportProductDTO()
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                            .OrderByDescending(n => n.Price)
                            .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            var usersAndProducts = new ExportUsersAndProductsDTO()
            {
                Count = context.Users.Count(x => x.ProductsSold.Any()),
                Users = users
            };

            string result = Serializer(typeof(ExportUsersAndProductsDTO), "Users", usersAndProducts);

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