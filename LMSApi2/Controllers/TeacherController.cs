using LMSApi2.Services.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSApi2.Models;

using LMSApi2.Authorization.AuthorizationTeacher;
using LMSApi2.DTOS.Instructors;

namespace LMSApi2.Controllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly IInstructorService _service;
        private readonly ILogger<Instructor> logger;

        public TeacherController(IInstructorService instructorService , ILogger<Instructor> logger) {
            _service = instructorService;
            this.logger = logger;
        }
        


        [HttpPost("[action]")]
        public IActionResult Login(AuthenticateRequestInstructor req) {
            string username = "mfazal";
            string password = "password";

            AuthenticateRequestInstructor request = new AuthenticateRequestInstructor() { username=username , password=password };
            logger.LogInformation("started");
            AuthenticateResponseInstructor instructorResponse = _service.AuthenticateLogin(request);
            Console.WriteLine("testing login");
            return Ok(instructorResponse);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Get() {
            //_service.addInstructor(new Instructor() { Id = "t1", Name = "munawwar fazal", UserName = "MunawwarFazal021", PasswordHash = "password" });

            //IEnumerable<Instructor> instrucutors = _service.GetInstructors();
            List<SubmissionFile> user = _service.Test();
            
            return Ok(user);

        }

        [HttpGet("Test")]
        public IActionResult Test() {
            _service.Test();
            return Ok();
        }

    }
}
