using LMSApi2.Models;
using System.Text.Json.Serialization;

namespace LMSApi2.DTOS.Users
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }



        public int Batch { get; set; }


        public UserDTO toDTO(User user) {
            return new UserDTO()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Batch = user.Batch
            };

        }
    }
}
