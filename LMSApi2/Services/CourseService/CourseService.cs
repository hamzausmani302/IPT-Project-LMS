using LMSApi2.DTOS.Courses;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly DataContext dataContext;
        public CourseService(DataContext context) {
            dataContext = context;
        }

        public async  Task<List<CourseDTO>> getAllCourses() {

            List<Course> courses = await dataContext.Courses.ToListAsync();
            List<CourseDTO> courseDTOs = new List<CourseDTO>();
            foreach (Course course in courses) {
                courseDTOs.Add(new CourseDTO(course));
            }


            return courseDTOs;
        
        }
    }
}
