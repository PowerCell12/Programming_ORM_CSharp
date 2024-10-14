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

            Console.WriteLine(ImportSuppliers(context, inputJson));
        }


        public static string ImportSuppliers(CarDealerContext context, string inputJson){


            var files = JsonSerializer.Deserialize<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(files);

            context.SaveChanges();

            return $"Successfully imported {files.Count}.";;
        }


        public static string ImportParts(CarDealerContext context, string inputJson){

            var parts = JsonSerializer.Deserialize<List<Part>>(inputJson);

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";;
        }


    }
}