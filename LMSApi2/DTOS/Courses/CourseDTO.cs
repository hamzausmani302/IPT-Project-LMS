using LMSApi2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSApi2.DTOS.Courses
{
    public class CourseDTO
    {

        public string? CourseID { get; set; }

        public string? CourseName { get; set; }

        public string? CourseDescription { get; set; }


        public int CreditHours { get; set; }

        public CourseType courseType { get; set; }


        public CourseDTO(Course course) { 
            CourseID = course.CourseID;
            CourseName = course.CourseName;
            CourseDescription = course.CourseDescription;
            CreditHours = course.CreditHours;
            courseType = course.courseType;
        }
    }
}
