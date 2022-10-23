using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.Models;

namespace LMSApi2.Services.ClassServices
{
    public interface IClassService
    {
        public List<ClassDTO> getClassesOfInstructor(Instructor instructor);
        public List<ClassDTO> getClassesOfUser(User user);
    }
}
