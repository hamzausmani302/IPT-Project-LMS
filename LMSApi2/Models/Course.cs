using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace LMSApi2.Models
{
    public enum CourseType { 
        Thoery,
        Lab
    }

    [Table("Courses")]
    public partial class Course
    {
        [Key]
        public string? CourseID { get; set; }
        [Required]
        public string? CourseName { get; set; }
        [MaxLength(300)]
        public string? CourseDescription { get; set; }
        
        [DefaultValue(3)]
        public int CreditHours { get; set; }

        public CourseType courseType { get; set; }

        

        public virtual List<Classes> Classes { get; set; }


        //public List<Instructor_Course> InstructorCourses { get; set; }

    }
}
