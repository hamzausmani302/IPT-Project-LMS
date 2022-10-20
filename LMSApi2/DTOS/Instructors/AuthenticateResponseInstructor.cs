using LMSApi2.Models;

namespace LMSApi2.DTOS.Instructors
{
    public class AuthenticateResponseInstructor
    {
        public string? Id { get; set; }
        
        public string? Name { get; set; }

        public string? UserName { get; set; }
        
        public FacultyType facultyType { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponseInstructor(Instructor teacher, string token)
        {
            Id = teacher.Id;
            Name = teacher.Name;
            UserName = teacher.UserName;
            facultyType = teacher.FacultyType;
            Token = token;
        }
    }
}
