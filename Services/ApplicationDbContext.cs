using System.Net.Http.Headers;
using final_project.Models;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Trainee> Trainees {get; set;}
        public DbSet<Department> Departments {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<Instructor> Instructors {get; set;}
        public DbSet<crsResult> crsResults {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainee>().ToTable("Trainees");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<crsResult>().ToTable("crsResults");

            modelBuilder.Entity<Instructor>()
               .HasOne(i => i.Dep) // Each Instructor has one Department
               .WithMany(d => d.Instructors) // Each Department has many Instructors
               .HasForeignKey(i => i.InstDepId) // Foreign key property in Instructor
               .OnDelete(DeleteBehavior.NoAction); // Restrict delete behavior

            modelBuilder.Entity<Course>()
                .HasOne(i => i.Dep)
                .WithMany(d => d.Courses)
                .HasForeignKey(i => i.DepId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trainee>()
                .HasOne(i => i.Dep)
                .WithMany(d => d.Trainees)
                .HasForeignKey(i => i.DepId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Department>()
            .HasMany(d => d.Trainees)
            .WithOne(t => t.Dep)
            .HasForeignKey(t => t.DepId) // Specify the foreign key if necessary
            .OnDelete(DeleteBehavior.NoAction);

                

        }



    }
}