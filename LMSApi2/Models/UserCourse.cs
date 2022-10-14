using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LMSApi2.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; } 
        public string userId { get; set; }
        public User User { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
