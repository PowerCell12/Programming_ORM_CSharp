using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using AutoMapper.QueryableExtensions;
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
            
            
            var inputXML = File.ReadAllText("Datasets/cars.xml");
            
            
            Console.WriteLine(ImportCars(context, inputXML));
        }

        public static Mapper Mapper(){
            var map = new MapperConfiguration(cfg => {
                cfg.CreateMap<SupplierDto, Supplier>();
                cfg.CreateMap<PartDto, Part>();
                cfg.CreateMap<CarDto, Car>();
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<SaleDto, Sale>();
                cfg.CreateMap<Car, ExportCar>();
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



        public static string ImportCars(CarDealerContext context, string inputXml){

            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("Cars"));

            using var reader = new StringReader(inputXml);

            CarDto[] cars = (CarDto[])serializer.Deserialize(reader);

            List<Car> final = new List<Car>();

            foreach( var carDto in cars){
                Car car = Mapper().Map<Car>(carDto);

                int[] carPratIds = carDto.PartsIds
                    .Select(x => x.Id)
                    .Distinct()
                    .ToArray();

                var carParts = new List<PartCar>();

                foreach (var id in carPratIds){
                    carParts.Add(new PartCar{
                        Car = car,
                        PartId = id
                    });
                }

                car.PartsCars = carParts;
                final.Add(car);
            }



            context.Cars.AddRange(final);

            context.SaveChanges();

            return $"Successfully imported {final.Count}"; 

        }



        public static string ImportCustomers(CarDealerContext context, string inputXml){

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));

            using var reader = new StringReader(inputXml);

            CustomerDto[] customers = (CustomerDto[])serializer.Deserialize(reader);
 
            var final  = Mapper().Map<Customer[]>(customers);

            context.Customers.AddRange(final);

            context.SaveChanges();

            return $"Successfully imported {final.Length}";;
        }

        public static string ImportSales(CarDealerContext context, string inputXml){

            var serializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("Sales"));

            using var reader = new StringReader(inputXml);

            SaleDto[] sales = (SaleDto[])serializer.Deserialize(reader);

            var carIds = context.Cars.Select(x => x.Id);

            var final = Mapper().Map<Sale[]>(sales.Where(x => carIds.Contains(x.CarId)));

            context.Sales.AddRange(final);

            context.SaveChanges();

            return $"Successfully imported {final.Length}";;
        }


        public static string GetCarsWithDistance(CarDealerContext context){
            var cars = context.Cars.Where(x => x.TraveledDistance > 2000000)
            .OrderBy(x => x.Make)
            .ThenBy(x => x.Model)
            .Take(10)
            .ProjectTo<ExportCar>(Mapper().ConfigurationProvider)
            .ToArray();


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCar[]), new XmlRootAttribute("cars"));

            var xsn = new XmlSerializerNamespaces();

            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new();

            using StringWriter writer =  new StringWriter(stringBuilder);
            xmlSerializer.Serialize(writer, cars, xsn);
            

            return stringBuilder.ToString().TrimEnd();

        }

    }
}