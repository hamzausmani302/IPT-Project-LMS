using LMSApi2.DTOS.Announcements;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.Models;

namespace LMSApi2.Services.ClassServices
{
    public interface IClassService
    {
        public List<ClassDTO> getClassesOfInstructor(Instructor instructor);
        public List<ClassDTO> getClassesOfUser(User user);
        public List<User> getUsersEnrolledInClass(int classId);

        public Task<ClassDTO> addUserToClass(string code , User user);
        public Classes addANewClass(AddClassDTO dto , Instructor instructor);

        public List<AnnouncementResponse> viewAnnoucements(int id);


        public bool isClassExists(int classId);
    }
}
