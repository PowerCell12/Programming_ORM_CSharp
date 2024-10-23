using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new();
            
            
            var inputXML = File.ReadAllText("Datasets/parts.xml");
            
            
            Console.WriteLine(ImportParts(context, inputXML));
        }

        public static Mapper Mapper(){
            var map = new MapperConfiguration(cfg => {
                cfg.CreateMap<SupplierDto, Supplier>();
                cfg.CreateMap<PartDto, Part>();
                
            });
            return new Mapper(map);
        }


        public static string ImportSuppliers(CarDealerContext context, string inputXml){

            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("Suppliers"));

            using var reader = new StringReader(inputXml);
            SupplierDto[] suplliers = (SupplierDto[])serializer.Deserialize(reader);
            
            Supplier[] suppliers = Mapper().Map<Supplier[]>(suplliers);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";


        }


        public static string ImportParts(CarDealerContext context, string inputXml){


            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("Parts"));

            using var reader = new StringReader(inputXml);

            PartDto[] parts = (PartDto[])serializer.Deserialize(reader);

            var supplierIds = context.Suppliers.Select(x => x.Id).ToArray();

            Part[] final = Mapper().Map<Part[]>(parts.Where(x => supplierIds.Contains(x.SupplierId)).ToArray());

            context.Parts.AddRange(final);

            context.SaveChanges();

            return $"Successfully imported {final.Length}";;
        }



    }
}