using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace LMSApi2.Models
{
    [Table("Courses")]
    public partial class Course
    {
        [Key]
        public string? CourseId { get; set; }
        [Required]
        public string? CourseName { get; set; }


       

        public User Teacher { get; set; }

        public virtual List<UserCourse> UserCourse { get; set; }


        public List<Instructor_Course> InstructorCourses { get; set; }

    }
}
