using System.Net.WebSockets;
using Newtonsoft.Json;
using ProductShop.Data;
// using ProductShop.Dtos;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            string data = File.ReadAllText("../../../Datasets/categories.json");
            Console.WriteLine(ImportProducts(context, data));
        }
    
    
        public static string ImportUsers(ProductShopContext context, string inputJson){
            List<User> json = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(json);

            context.SaveChanges();
            return $"Successfully imported {json.Count()}";
        }


        public static string ImportProducts(ProductShopContext context, string inputJson){
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            if (products != null){

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"Successfully imported {products?.Length}";
        
        }


        public static string ImportCategories(ProductShopContext context, string inputJson){

            var json = JsonConvert.DeserializeObject<List<Category>>(inputJson).Where(x => x.Name != null).ToList();


            if (json != null){
                context.Categories.AddRange(json);
                context.SaveChanges();
            }

            return  $"Successfully imported {json.Count}";

        }




    }
}