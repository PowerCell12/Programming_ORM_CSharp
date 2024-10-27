using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data { 

    public class HospitalContext: DbContext{


        public HospitalContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }

        private const string  ConnectionString = "Server=.;Database=FootballBetting;TrustServerCertificate=True;User Id=sa;Password=eMk54#k49K<";


        public virtual DbSet<Diagnose> Diagnoses {get; set;}
        public virtual DbSet<Doctor> Doctors {get; set;}
        public virtual DbSet<Medicament> Medicaments {get; set;}
        public virtual DbSet<Patient> Patients {get; set;}
        public virtual DbSet<PatientMedicament> Prescriptions {get; set;}
        public virtual DbSet<Visitation> Visitations {get; set;}


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(ConnectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicament>().HasKey(x => new {x.MedicamentId, x.PatientId});
        }


    }




}