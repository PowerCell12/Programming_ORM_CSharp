using System.Diagnostics.Contracts;
using System.Text.Json;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {

            ProductShopContext context = new();

            string inputJson = File.ReadAllText("Datasets/users.json");

            Console.WriteLine(GetProductsInRange(context));
        }

        public static string ImportUsers(ProductShopContext context, string inputJson){

            var jsonObjects = JsonSerializer.Deserialize<List<User>>(inputJson);

            context.Users.AddRange(jsonObjects);

            context.SaveChanges();

            return $"Successfully imported {jsonObjects.Count}";

        }

        public static string ImportProducts(ProductShopContext context, string inputJson){

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(inputJson);

            if (products != null){
                context.Products.AddRange(products);

                context.SaveChanges();
            }

            return $"Successfully imported {products?.Count}";

        }



        public static string ImportCategories(ProductShopContext context, string inputJson){

            var categories = JsonSerializer.Deserialize<List<Category>>(inputJson).Where(x => x.name != null).ToList();

            context.AddRange(categories);

            context.SaveChanges();
            return $"Successfully imported {categories.Count}";


        }


        public static string ImportCategoryProducts(ProductShopContext context, string inputJson){

            var CategoriesProducts = JsonSerializer.Deserialize<List<CategoryProduct>>(inputJson);

            context.AddRange(CategoriesProducts);

            context.SaveChanges();

            return $"Successfully imported {CategoriesProducts.Count}";
        }


        public static string GetProductsInRange(ProductShopContext context){

            var products  = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
            .Select(x => new {
                name = x.Name,
                price = x.Price,
                seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
            })
            .OrderBy(x => x.price);

          

            return JsonSerializer.Serialize(products, new JsonSerializerOptions {
                WriteIndented = true
            });

        }

    }
}