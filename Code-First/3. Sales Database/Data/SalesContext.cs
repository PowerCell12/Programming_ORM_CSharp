using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data{

    public class SalesContext : DbContext{


        public SalesContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }

        private const string  ConnectionString = "Server=.;Database=FootballBetting;TrustServerCertificate=True;User Id=sa;Password=eMk54#k49K<";

        public virtual DbSet<Customer> Customers {get; set;}
        public virtual DbSet<Product> Products {get; set;}
        public virtual DbSet<Sale> Sales {get; set;}
        public virtual DbSet<Store> Stores {get; set;}


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
            //     optionsBuilder.UseSqlServer(ConnectionString);
        // }
    }


}