using System.Text.Json;
using System.Text.Json.Serialization;
using CarDealer.Data;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new();
            string inputJson = File.ReadAllText("Datasets/suppliers.json");

            Console.WriteLine(ImportParts(context, inputJson));
        }


        public static string ImportSuppliers(CarDealerContext context, string inputJson){


            var files = JsonSerializer.Deserialize<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(files);

            context.SaveChanges();

            return $"Successfully imported {files.Count}.";;
        }


        public static string ImportParts(CarDealerContext context, string inputJson){

            var parts = JsonSerializer.Deserialize<List<Part>>(inputJson);

            var supplierIds = context.Suppliers.Select(x => x.Id).ToArray();

            var partsWithValidSupploiers = parts.Where(p => supplierIds.Contains(p.SupplierId)).ToList();

            context.Parts.AddRange(partsWithValidSupploiers);

            context.SaveChanges();

            return $"Successfully imported {partsWithValidSupploiers.Count}.";;
        }


        public static string ImportCars(CarDealerContext context, string inputJson){

            var cars = JsonSerializer.Deserialize<List<Car>>(inputJson);

            context.Cars.AddRange(cars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }


    }
}