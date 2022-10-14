using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSApi2.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }

        public List<Instructor_Course> InstructorCourses { get; set; }
    }
}
