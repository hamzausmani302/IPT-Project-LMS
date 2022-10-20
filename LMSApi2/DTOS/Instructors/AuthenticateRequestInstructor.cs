using System.ComponentModel.DataAnnotations;

namespace LMSApi2.DTOS.Instructors
{
    public class AuthenticateRequestInstructor
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }


    }
}
