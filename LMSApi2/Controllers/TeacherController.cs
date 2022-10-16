using LMSApi2.Services.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSApi2.Models;
using LMSApi2.DTOS;

namespace LMSApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly IInstructorService _service;

        public TeacherController(IInstructorService instructorService) {
            _service = instructorService;
        }

        [HttpGet("[action]")]
        public IActionResult Get() {
            //_service.addInstructor(new Instructor() { Id = "t1", Name = "munawwar fazal", UserName = "MunawwarFazal021", PasswordHash = "password" });

            //IEnumerable<Instructor> instrucutors = _service.GetInstructors();
            List<SubmissionFile> user = _service.Test();
            
            return Ok(user);

        }

    }
}
