using LMSApi2.DTOS.Courses;
using LMSApi2.Services.CourseService;
using Microsoft.AspNetCore.Mvc;

namespace LMSApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        
        private readonly ICourseService courseService;
        public CourseController(ICourseService courseService) {
            this.courseService = courseService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            List<CourseDTO> courses = await courseService.getAllCourses();
            
            return Ok(courses);
            
        }
    }
}
