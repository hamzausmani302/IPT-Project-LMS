using LMSApi2.Models;

namespace LMSApi2.Services.Teachers
{
    public interface IInstructorService
    {
        public IEnumerable<Instructor> GetInstructors();
        public Instructor addInstructor(Instructor instructor);

    }
}
