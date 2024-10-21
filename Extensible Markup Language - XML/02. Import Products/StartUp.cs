using System.IdentityModel.Tokens.Jwt;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.Execution;
using ProductShop.Data;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {

        }

        public static Mapper Mapper(){
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<ProductDto, Product>();
            });

            return new Mapper(mapper);
        }


        public static string ImportUsers(ProductShopContext context, string inputXml){


            var serliazer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));

            var reader = new StringReader(inputXml);

            UserDto[] users = (UserDto[])serliazer.Deserialize(reader);

            var mapped = Mapper().Map<User[]>(users);

            context.Users.AddRange(mapped);

            context.SaveChanges();

            return $"Successfully imported {mapped.Length}";;
        }

        public static string ImportProducts(ProductShopContext context, string inputXml){

            var serliazer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("Products"));

            var reader = new StringReader(inputXml);

            ProductDto[] products = (ProductDto[])serliazer.Deserialize(reader);

            var mapped = Mapper().Map<Product[]>(products);

            context.Products.AddRange(mapped);

            context.SaveChanges();

            return $"Successfully imported {mapped.Length}";;
        }
    }
}