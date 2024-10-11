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
            string data = File.ReadAllText("Datasets/users.json");
            Console.WriteLine(ImportUsers(context, data));
        }
    
    
        public static string ImportUsers(ProductShopContext context, string inputJson){
            List<User> json = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(json);

            context.SaveChanges();
            return $"Successfully imported {json.Count()}";
        }




    }
}