using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;


namespace P02_FootballBetting.Data{


    public class FootballBettingContext: DbContext {

        public FootballBettingContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {

        }


        private const string  ConnectionString = "Server=.;Database=FootballBetting;TrustServerCertificate=True;User Id=sa;Password=eMk54#k49K<";

        public virtual DbSet<Bet> Bets {get;set;}
        
        public virtual DbSet<Color> Colors {get; set;}

        public virtual DbSet<Country> Countries {get; set;}

        public virtual DbSet<Game> Games {get; set;}

        public virtual DbSet<Player> Players {get; set;}

        public virtual DbSet<PlayerStatistic> PlayersStatistics {get; set;}

        public virtual DbSet<Position> Positions {get; set;}

        public virtual DbSet<Team> Teams {get; set;}

        public virtual DbSet<Town> Towns {get; set;}

        public virtual DbSet<User> Users {get; set;}


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(ConnectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStatistic>().HasKey(x => new {x.GameId, x.PlayerId});
        }

    }



}