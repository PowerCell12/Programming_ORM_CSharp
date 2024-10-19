using System.Globalization;
using System.Text.Json;
using CarDealer.Data;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            CarDealerContext context = new();

            Console.WriteLine(GetOrderedCustomers(context));

        }


         public static string GetOrderedCustomers(CarDealerContext context){

            
            var customers = context.Customers
            .OrderBy(x => x.BirthDate)
            .ThenBy(x => x.IsYoungDriver == true)
            .Select(x => new {
                Name = x.Name,
                BirthDate = x.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                IsYoungDriver = x.IsYoungDriver
            });



            return JsonSerializer.Serialize(customers, new JsonSerializerOptions{
                WriteIndented = true
            }); 
        }
    }
}