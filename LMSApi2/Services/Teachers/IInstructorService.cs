using LMSApi2.DTOS;
using LMSApi2.DTOS.Instructor;
using LMSApi2.Models;

namespace LMSApi2.Services.Teachers
{
    public interface IInstructorService
    {

        /*        public IEnumerable<Instructor> GetInstructors();
                public Instructor addInstructor(Instructor instructor);

                public List<UserClassModel> GetAllClasses(string userId);*/
        public IEnumerable<Instructor> GetInstructors();
        public List<SubmissionFile> Test();
        public Instructor addInstructor(TeacherAuthReq obj);
        public bool removeInstructor(Guid _id);

        public Instructor GetInstructor(Guid _id);
        public Instructor updateInstructor(Instructor _instructor);


    }
}
