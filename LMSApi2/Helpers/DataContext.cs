using LMSApi2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace LMSApi2.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        //public DbSet<UserCourse> UsersCourses { get; set; }

       public DbSet<Instructor> Instructor { get; set; }
        //public DbSet<Instructor_Course> InstructorCourses { get; set; }

        public DbSet<Classes> _Classes { get; set; }
        public DbSet<SubmissionFile> SubmissionFile { get; set; }
        //public DbSet<StudentClasses> studentsClasses {get;set;}
        public DbSet<ClassesUser> ClassesUsers { get; set; }

        private readonly IConfiguration Configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;

        public DataContext(IConfiguration configuration , Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            environment = env;
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            string serverAlias = "Default";
            if (!environment.IsDevelopment()) {
                serverAlias = "RemoteServer";
            }
            
            options.UseSqlServer(Configuration.GetConnectionString(serverAlias));



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentClasses>().HasKey(sc => new { sc.StudentId, sc.ClassID });
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId="k190146",
                   Batch= 2021,
                   Email="hamza@gmail.com",
                   FirstName="hamza",
                   LastName="usmani",
                   PasswordHash="password",
                   Role=Role.User,
                   
               });

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId = "k190220",
                   Batch = 2022,
                   Email = "k190220@=nu.edu.pk",
                   FirstName = "ibad",
                   LastName = "saleem",
                   PasswordHash = "password",
                   Role = Role.User,

               }
   );
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId = "k190121",
                   Batch = 2021,
                   Email = "test@gmail.com",
                   FirstName = "Test",
                   LastName = "test",
                   PasswordHash = "password",
                   Role = Role.User,
               }

   );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    UserName="murtazafazal",
                    FacultyType=FacultyType.Visiting,
                    Id="t003",
                    Name="murtaza fazal",
                    PasswordHash="password"
                }
                );

       
            base.OnModelCreating(modelBuilder);
        }
    }
}