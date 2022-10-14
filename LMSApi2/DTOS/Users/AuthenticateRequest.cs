using System.ComponentModel.DataAnnotations;
namespace LMSApi2.DTOS.Users
{


    public class AuthenticateRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
