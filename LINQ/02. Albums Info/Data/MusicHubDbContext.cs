
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;

namespace MusicHub.Data{


    public class MusicHubDbContext: DbContext{
            
        public MusicHubDbContext(DbContextOptions<MusicHubDbContext> options)
            : base(options)
        {
        }

        public MusicHubDbContext()
        {
        }

        
        public virtual DbSet<Song> Songs {get; set;} = null!;

        public virtual DbSet<Album> Albums {get; set;} = null!;

        public virtual DbSet<Performer> Performers {get; set;} = null!;

        public virtual DbSet<Producer> Producers {get; set;} = null!;

        public virtual DbSet<Writer> Writers {get; set;} = null!;

        public virtual DbSet<SongPerformer> SongsPerformers {get; set;} = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                        if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MusicHub;TrustServerCertificate=True;User Id=sa;Password=eMk54#k49K<");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<SongPerformer>().HasKey(x => new {x.SongId, x.PerformerId});

        }

        

    }


}