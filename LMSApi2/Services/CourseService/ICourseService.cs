using LMSApi2.DTOS.Courses;

namespace LMSApi2.Services.CourseService
{
    public interface ICourseService
    {

        public Task<List<CourseDTO>> getAllCourses();


    }
}
