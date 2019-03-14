namespace BookShop
{
    using Data;
    using Initializer;
    using Models.Enums;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);

                int removedBooks = RemoveBooks(db);

                Console.WriteLine($"{removedBooks} books were deleted");
            }
        }

        //01. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction ageRestriction;
            if (!Enum.TryParse<AgeRestriction>(command, true, out ageRestriction))
            {
                return "";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            string result = string.Join(Environment.NewLine, books);

            return result;
        }

        //02. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    BookId = b.BookId,
                    Title = b.Title
                })
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            string result = string.Join(Environment.NewLine, books);

            return result;
        }

        //03. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,

                    Price = b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        //04. Not Released In
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //05. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] bookCategories = input.Split().Select(s => s.ToLower()).ToArray();

            var books = context.Books
                .Where(b => b.BookCategories
                    .Any(bc => bookCategories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(t => t.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //06. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    EditionType = Convert.ToString(b.EditionType),
                    b.Price
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}"));
        }

        //07. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            return string.Join(Environment.NewLine, authors.Select(a => a.FullName));
        }

        //08. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //09. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(ba => new
                {
                    ba.Title,
                    AuthorName = ba.Author.FirstName + " " + ba.Author.LastName
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} ({b.AuthorName})"));
        }

        //10. Count Books 
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList();

            return books.Count;
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToList();


            return string.Join(Environment.NewLine, authors.Select(b => $"{b.FirstName} {b.LastName} - {b.Copies}"));
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Price * cb.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            return string.Join(Environment.NewLine, categories.Select(c => $"{c.Name} ${c.Profit}"));
        }

        //13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks.Select(b => new
                    {
                        b.Book.Title,
                        b.Book.ReleaseDate
                    })
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                })
                .OrderBy(c => c.Name)
                .ToList();

            return string.Join(Environment.NewLine,
                categories.Select(c =>
                    $"--{c.Name}{Environment.NewLine}{string.Join(Environment.NewLine, c.Books.Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))}"));
        }

        //14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.Books.UpdateRange(books);
            context.SaveChanges();
        }

        //15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.RemoveRange(books);
            context.SaveChanges();

            //return context.SaveChanges(); this don't work in judge

            return books.Count();
        }
    }
}