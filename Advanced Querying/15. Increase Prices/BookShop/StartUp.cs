namespace BookShop
{
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.Json.Nodes;
    using System.Xml.Linq;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);

            // string input  = Console.ReadLine();

            Console.WriteLine(CountCopiesByAuthor(db));

        }



        public static string GetBooksByAgeRestriction(BookShopContext context, string command){ // done
            string command1 =  command.ToLower();
            string command2 = command1.Substring(0, 1).ToUpper() + command1.Substring(1); 
            var age =  (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command2);


            var books = context.Books.AsNoTracking().Where(x => x.AgeRestriction == age).Select(x => new {
                x.Title
            }).OrderBy(x => x.Title);

           return string.Join("\n", books.Select(x => x.Title));
        }








        public static string GetGoldenBooks(BookShopContext context){ // done

            var books = context.Books.AsNoTracking().Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000)
            .Select(x => new {
                x.BookId,
                x.Title
            })
            .OrderBy(x => x.BookId);

            return string.Join("\n", books.Select(x => x.Title));

        }









        public static string GetBooksByPrice(BookShopContext context){ // done

            var books = context.Books.AsNoTracking().Where(x => x.Price > 40)
            .Select(x => new {
                x.Title,
                x.Price
            }).OrderByDescending(x => x.Price);

            return string.Join("\n", books.Select(x => $"{x.Title} - ${x.Price:F2}"));

        }














    
        public static string GetBooksNotReleasedIn(BookShopContext context, int year){ // done

            var books = context.Books.AsNoTracking().Where(x => x.ReleaseDate.Value.Year != year)
            .Select(x => new {
                x.Title,
                x.BookId
            })
            .OrderBy(x => x.BookId);

            return string.Join("\n", books.Select(x => x.Title));

        }














        public static string GetBooksByCategory(BookShopContext context, string input){ // done
            List<string> genres  = input.Split(" ").Select(x => x.ToLower()).ToList();

            var books = context.Books.AsNoTracking().Include(x => x.BookCategories).ThenInclude(x => x.Category)
                .Where(x => x.BookCategories.Any(bc => genres.Contains(bc.Category.Name.ToLower())))
                .OrderBy(x => x.Title);

            return string.Join("\n", books.Select( x => x.Title));
        }


















        public static string GetBooksReleasedBefore(BookShopContext context, string date){ // done

            var books = context.Books.AsNoTracking().Where(x => x.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
            .Select(x => new {
                x.Title,
                x.EditionType,
                x.Price,
                x.ReleaseDate
            })
            .OrderByDescending(x => x.ReleaseDate);



            return string.Join("\n", books.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
        }












        public static string GetAuthorNamesEndingIn(BookShopContext context, string input){ // done


            var authors = context.Authors.AsNoTracking().Where( x => x.FirstName.EndsWith(input))
            .Select(x => new {
                FullName = x.FirstName + " " + x.LastName
            })
            .OrderBy(x => x.FullName);

            return string.Join("\n", authors.Select(x => x.FullName));
        }
















        
        public static string GetBookTitlesContaining(BookShopContext context, string input){ // done

            var books = context.Books.AsNoTracking().Where(x => x.Title.ToLower().Contains(input.ToLower()))
            .Select(x => new {
                x.Title,
            })
            .OrderBy(x => x.Title);

            return string.Join("\n", books.Select(x => x.Title));
        }










        public static string GetBooksByAuthor(BookShopContext context, string input){ // done

            
            var authors = context.Books.AsNoTracking().Include(x => x.Author).Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
            .Select(x => new {
                x.Title,
                x.Author.FirstName,
                x.Author.LastName,
                x.BookId
            })
            .OrderBy(x => x.BookId);

            return string.Join("\n", authors.Select(x => $"{x.Title} ({x.FirstName} {x.LastName})"));
        }








        public static int CountBooks(BookShopContext context, int lengthCheck){ // done

            int count = context.Books.Where(x => x.Title.Length > lengthCheck).Count();
            return count;


        }


        public static string CountCopiesByAuthor(BookShopContext context){ //  done

            var authors  = context.Authors.AsNoTracking().Include(x => x.Books)
            .Select(x => new {

                x.FirstName,
                x.LastName,
                BookCount = x.Books.Sum(x => x.Copies)

            }).OrderByDescending(x => x.BookCount);

            return string.Join("\n", authors.Select(x => $"{x.FirstName} {x.LastName} - {x.BookCount}"));
        }








        public static string GetTotalProfitByCategory(BookShopContext context){  // done
 
            var categories = context.Categories.AsNoTracking().Include(x => x.CategoryBooks).ThenInclude(x => x.Book)
            .Select(x => new {
                x.Name,
                TotalPrice = x.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies)
            })
            .OrderByDescending(x => x.TotalPrice)
            .ThenBy(x => x.Name);

            return string.Join("\n", categories.Select(x => $"{x.Name} ${x.TotalPrice}"));
        }










        public static string GetMostRecentBooks(BookShopContext context){ // 

            var cat = context.Categories.AsNoTracking().Include(x => x.CategoryBooks).ThenInclude(x => x.Book)
            .Select(x => new {
                x.Name, 
                Books = x.CategoryBooks.OrderByDescending(x => x.Book.ReleaseDate).Take(3).Select(x => new {
                    Title = x.Book.Title,
                    ReleaseDate = x.Book.ReleaseDate
                })
            })
            .OrderBy(x => x.Name);

            return string.Join("\n", cat.Select(x => $"--{x.Name}\n{string.Join("\n", x.Books.Select(y => $"{y.Title} ({y.ReleaseDate.Value.Year})"))}"));

        }






        public static void IncreasePrices(BookShopContext context){

            var books = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010);

            foreach (var book in books){            
                book.Price += 5;
            }

            context.SaveChanges();


        }



        public static int RemoveBooks(BookShopContext context){

            int count = context.Books.Where(x => x.Copies < 4200).Count();
            var books = context.Books.Where(x => x.Copies < 4200);

            context.Books.RemoveRange(books);

            return count;

        }


    }
}


