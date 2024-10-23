using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using CarDealer.Data;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();
            string xml = File.ReadAllText("Datasets/suppliers.xml");

            Console.WriteLine(ImportSuppliers(context, xml));

        }


        public static string ImportSuppliers(CarDealerContext context, string inputXml){

            XDocument document = XDocument.Parse(inputXml);
            int count = 0;

            foreach(var element1 in document.Elements()){

                foreach( var element in element1.Elements()){
                    var el1 = element.Element("name").Value;
                    var el2 = element.Element("isImporter").Value;

                    Supplier supplier = new()
                    {
                        Name = el1,
                        IsImporter = el2 == "true"
                    };

                    context.Suppliers.Add(supplier);
                    context.SaveChanges();


                    count++;
                }
            }

            return $"Successfully imported {count}";;
        }

    }
}