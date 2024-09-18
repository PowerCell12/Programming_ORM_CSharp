using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data{

    public class StudentSystemContext: DbContext{

        public StudentSystemContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }

        private const string  ConnectionString = "Server=.;Database=FootballBetting;TrustServerCertificate=True;User Id=sa;Password=eMk54#k49K<";


        public virtual DbSet<Course> Courses {get; set;}
        public virtual DbSet<Homework> Homeworks {get; set;}
        public virtual DbSet<Resource> Resources {get; set;}
        public virtual DbSet<Student> Students {get; set;}
        public virtual DbSet<StudentCourse> StudentsCourses {get; set;}


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(ConnectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(x => new {x.CourseId, x.StudentId});
        }

    }


}