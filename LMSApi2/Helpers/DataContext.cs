using LMSApi2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace LMSApi2.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<UserCourse> UsersCourses { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Instructor_Course> InstructorCourses { get; set; }


        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
