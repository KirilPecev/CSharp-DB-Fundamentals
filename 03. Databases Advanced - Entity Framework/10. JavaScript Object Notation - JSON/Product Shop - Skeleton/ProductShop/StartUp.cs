namespace ProductShop
{
    using Data;
    using Newtonsoft.Json;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            string inputJson =
                File.ReadAllText(
                    @"C:\C#\C# DB Fundamentals\03. Databases Advanced - Entity Framework\10. JavaScript Object Notation - JSON\Product Shop - Skeleton\ProductShop\Datasets\categories-products.json");

            string result = GetUsersWithProducts(context);

            string path =
                @"C:\C#\C# DB Fundamentals\03. Databases Advanced - Entity Framework\10. JavaScript Object Notation - JSON\Product Shop - Skeleton\ProductShop\Datasets\users-and-products.json";

            File.WriteAllText(path, result);

            Console.WriteLine(result);
        }

        //Query 1. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {context.Users.Count()}";
        }

        //Query 2. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {context.Products.Count()}";
        }

        //Query 3. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(p => p.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {context.Categories.Count()}";
        }

        //Query 4. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {

            List<CategoryProduct> categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {context.CategoryProducts.Count()}";
        }

        //Query 5. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                });

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        //Query 6. Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                });

            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }

        //Query 7. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = $"{(c.CategoryProducts.Select(p => p.Product.Price).Sum() / c.CategoryProducts.Count):F2}",
                    totalRevenue = $"{(c.CategoryProducts.Select(p => p.Product.Price).Sum()):F2}"
                });

            return JsonConvert.SerializeObject(categories, Formatting.Indented);
        }

        //Query 8. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var obj = new
            {
                usersCount = context.Users.Count(us => us.ProductsSold.Count > 0),
                users = context.Users
                    .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                    .OrderByDescending(u => u.ProductsSold.Count(p => p.Buyer != null))
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count(p => p.Buyer != null),
                            products = u.ProductsSold
                                .Where(p => p.Buyer != null)
                                .Select(p => new
                                {
                                    name = p.Name,
                                    price = p.Price
                                })
                        }
                    })
            };

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}