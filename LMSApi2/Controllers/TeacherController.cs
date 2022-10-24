using LMSApi2.Services.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSApi2.Models;

using LMSApi2.Authorization.AuthorizationTeacher;
using LMSApi2.DTOS.Instructors;
using LMSApi2.Services.ClassServices;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.DTOS.Users;

namespace LMSApi2.Controllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly IInstructorService _service;
        private readonly ILogger<Instructor> logger;
        private readonly IClassService _classService;

        public TeacherController(IInstructorService instructorService , IClassService classService , ILogger<Instructor> logger) {
            _service = instructorService;
            this.logger = logger;
            _classService = classService;
        }
        


        [HttpPost("[action]")]
        public IActionResult Login(AuthenticateRequestInstructor req) {
            

            AuthenticateRequestInstructor request = new AuthenticateRequestInstructor() { username=req.username , password=req.password };
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
            
            
            return Ok("OK");

        }

        [Authorize]
        [HttpGet("classes")]
        public IActionResult GetClasses()
        {
            //User currentUser = (User)HttpContext.Items["User"];
            const string V = "Instructor";
            Instructor currentUser = (Instructor)HttpContext.Items[V];
            
          
            List<ClassDTO> classes = _classService.getClassesOfInstructor(currentUser);




            return Ok(classes);
        }

        [Authorize]
        [HttpGet("class/students/{id}")]
        public IActionResult ViewStudents(string id) {
            int cid;
            Int32.TryParse(id, out cid);
            List<User> students = _classService.getUsersEnrolledInClass(cid );
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (User user in students) {
                userDTOs.Add(new UserDTO().toDTO(user));
            }
           


            return Ok(userDTOs);
        }

        [HttpPost("class/add")]
        public IActionResult createClass(AddClassDTO addClassDTO) {

            _classService.addANewClass(addClassDTO);
            return Ok("done");
        }

        [HttpGet("Test")]
        public IActionResult Test() {
            List<ClassDTO> clss = _service.Test();
            return Ok(clss);
        }

    }
}
