using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static string GetCarsFromMakeToyota(CarDealerContext context){

            var cars = context.Cars.Where(x => x.Make == "Toyota")
            .Select(x => new {
                x.Id,
                x.Make,
                x.Model,
                x.TraveledDistance
            })
            .OrderBy(x => x.Model)
            .ThenByDescending(x => x.TraveledDistance);

            return JsonSerializer.Serialize(cars, new JsonSerializerOptions{
                WriteIndented = true
            });
        }
    
    
        public static string GetTotalSalesByCustomer(CarDealerContext context){

            var customers = context.Customers.Where(x => x.Sales.Count() >= 1)
            .Select(x => new {
                fullName = x.Name,
                boughtCars = x.Sales.Count(),
                spentMoney = x.Sales.Sum(y => y.Car.PartsCars.Sum(z => z.Part.Price))
            })
            .OrderByDescending(x => x.spentMoney)
            .ThenByDescending(x => x.boughtCars);


            return JsonSerializer.Serialize(customers, new JsonSerializerOptions{
                WriteIndented = true
            });

        }
    }
}