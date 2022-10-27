using LMSApi2.DTOS;
using LMSApi2.DTOS.Announcements;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.DTOS.Instructors;
using LMSApi2.Models;

namespace LMSApi2.Services.Teachers
{
    public interface IInstructorService
    {

        /*        public IEnumerable<Instructor> GetInstructors();
                public Instructor addInstructor(Instructor instructor);

                public List<UserClassModel> GetAllClasses(string userId);*/
        public List<ClassDTO> Test();

        public AuthenticateResponseInstructor AuthenticateLogin(AuthenticateRequestInstructor request);

        public Instructor getInstructorById(string? userId);

        public AnnouncementResponse addAnnouncementInClass(int classId , AnnouncementCreateDTO dto);

    }
}
