using System.ComponentModel.DataAnnotations;
namespace LMSApi2.DTOS.Instructor
{
    public class TeacherAuthReq
    {
 
        [Required]
        public string Name { get; set; }

        [Required]
        public string  Password { get; set; }

    }
}
