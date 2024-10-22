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
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<CategoryProductDto, CategoryProduct>();
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


        public static string ImportCategories(ProductShopContext context, string inputXml){

            var serliazer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            var reader = new StringReader(inputXml);

            CategoryDto[] categories = (CategoryDto[])serliazer.Deserialize(reader);

            var mapped = Mapper().Map<Category[]>(categories.Where(x => x.Name != null));

            context.Categories.AddRange(mapped);

            context.SaveChanges();

            return $"Successfully imported {mapped.Length}";;
        }


        public static string ImportCategoryProducts(ProductShopContext context, string inputXml) {

            var serliazer = new XmlSerializer(typeof(CategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var reader = new StringReader(inputXml);

            CategoryProductDto[] categoryProducts = (CategoryProductDto[])serliazer.Deserialize(reader);

            var productIds  = context.Products.Select(x => x.Id);

            var CategoryIds = context.Categories.Select(x => x.Id);


            var mapped = Mapper().Map<CategoryProduct[]>(categoryProducts.Where(x => productIds.Contains(x.ProductId) && CategoryIds.Contains(x.CategoryId)));

            context.CategoryProducts.AddRange(mapped);

            context.SaveChanges();

            return $"Successfully imported {mapped.Length}";
        }



    }
}