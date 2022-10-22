using LMSApi2.Models;

namespace LMSApi2.DTOS.Instructors
{
    public class InstrutorDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }

        public FacultyType FacultyType { get; set; }

        public InstrutorDTO(Instructor ins) {
            Id = ins.Id;
            Name = ins.Name;
            UserName = ins.UserName;
            PasswordHash = ins.PasswordHash;    
            FacultyType = ins.FacultyType;
        }
        public InstrutorDTO() { }
    }
}
