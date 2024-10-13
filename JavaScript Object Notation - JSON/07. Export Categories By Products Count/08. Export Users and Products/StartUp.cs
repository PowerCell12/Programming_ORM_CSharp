using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Castle.DynamicProxy;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

            Console.WriteLine(GetUsersWithProducts(context));
        }

        // public static string ImportUsers(ProductShopContext context, string inputJson){

        //     var jsonObjects = JsonSerializer.Deserialize<List<User>>(inputJson);

        //     context.Users.AddRange(jsonObjects);

        //     context.SaveChanges();

        //     return $"Successfully imported {jsonObjects.Count}";

        // }

        // public static string ImportProducts(ProductShopContext context, string inputJson){

        //     List<Product> products = JsonSerializer.Deserialize<List<Product>>(inputJson);

        //     if (products != null){
        //         context.Products.AddRange(products);

        //         context.SaveChanges();
        //     }

        //     return $"Successfully imported {products?.Count}";

        // }



        // public static string ImportCategories(ProductShopContext context, string inputJson){

        //     var categories = JsonSerializer.Deserialize<List<Category>>(inputJson).Where(x => x.name != null).ToList();

        //     context.AddRange(categories);

        //     context.SaveChanges();
        //     return $"Successfully imported {categories.Count}";


        // }


        // public static string ImportCategoryProducts(ProductShopContext context, string inputJson){

        //     var CategoriesProducts = JsonSerializer.Deserialize<List<CategoryProduct>>(inputJson);

        //     context.AddRange(CategoriesProducts);

        //     context.SaveChanges();

        //     return $"Successfully imported {CategoriesProducts.Count}";
        // }


        // public static string GetProductsInRange(ProductShopContext context){

        //     var products  = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
        //     .Select(x => new {
        //         name = x.Name,
        //         price = x.Price,
        //         seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
        //     })
        //     .OrderBy(x => x.price);

          

        //     return JsonSerializer.Serialize(products, new JsonSerializerOptions {
        //         WriteIndented = true
        //     });

        // }


        // public static string GetSoldProducts(ProductShopContext context){

        //     var products = context.Users.Where(x => x.ProductsSold.Any(x => x.BuyerId != null) ) // byer
        //     .OrderBy(x => x.LastName)
        //     .ThenBy(y => y.FirstName)
        //     .Select(x => new {
        //         firstName = x.FirstName,
        //         lastName = x.LastName,
        //         soldProducts = x.ProductsSold.Select(y => new {
        //             name = y.Name,
        //             price =  y.Price,
        //             buyerFirstName = y.Buyer.FirstName,
        //             buyerLastName = y.Buyer.LastName
        //         })
        //     });




        //     return JsonSerializer.Serialize(products, new JsonSerializerOptions{
        //         WriteIndented = true
        //     });
        // }






        // public static string GetCategoriesByProductsCount(ProductShopContext context){

        //     var categories = context.Categories
        //     .Select(x => new {
        //         category = x.name,
        //         productsCount = x.CategoriesProducts.Count(),
        //         averagePrice = $"{x.CategoriesProducts.Average(x => x.Product.Price):F2}",
        //         totalRevenue = $"{x.CategoriesProducts.Sum(x => x.Product.Price):F2}"
        //     })
        //     .OrderByDescending(x => x.productsCount);

        //     return JsonSerializer.Serialize(categories, new JsonSerializerOptions{
        //         WriteIndented= true
        //     });
        // }


        public static string GetUsersWithProducts(ProductShopContext context){

            var users = context.Users.Where(x => x.ProductsSold.Any(x => x.BuyerId != null) ) //
            .Select(x => new   {
                firstName = x.FirstName,
                lastName = x.LastName,
                age = x.age,
                soldProducts = x.ProductsSold
                    .Where(x => x.BuyerId != null)
                    .Select( p => new {
                        name = p.Name,
                        price = p.Price
                    })
            })
            .OrderByDescending( x => x.soldProducts.Count());

            var output = new {

                usersCount = users.Count(),
                users = users.Select(x => new {
                    x.firstName,
                    x.lastName,
                    x.age,
                    soldProducts = new {
                        count = x.soldProducts.Count(),
                        products = x.soldProducts
                    }
                })
            };


            return JsonConvert.SerializeObject(output, new JsonSerializerSettings{
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
        }


    }
}