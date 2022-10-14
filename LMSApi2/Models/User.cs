using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMSApi2.Models
{
   
    public  class User
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public Role Role { get; set; }

       
       
        [JsonIgnore]
        public string? PasswordHash { get; set; }

        public virtual List<UserCourse> UserCourse { get; set; }
        
    }
}
