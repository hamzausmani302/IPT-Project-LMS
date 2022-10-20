using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSApi2.Models
{
    public enum FacultyType { 
        Visiting,
        Permanent
    }

    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }

        public FacultyType FacultyType { get; set; }


        public virtual List<Classes> InstructorClasses { get; set; }
    }
}
