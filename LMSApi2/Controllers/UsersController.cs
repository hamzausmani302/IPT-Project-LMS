using LMSApi2.Authorization.AuthorizationUser;
using LMSApi2.Authorization.AuthorizationAnonymous;
using LMSApi2.DTOS.Users;
using LMSApi2.Models;
using LMSApi2.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSApi2.Services.ClassServices;
using LMSApi2.DTOS.ClassesDTO;

namespace LMSApi2.Controllers
{
   
    [Route("api/user/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IClassService _classService;
        private readonly ILogger<User> logger;
        public UsersController(IUserService userService, IClassService classService , ILogger<User> logger) {
            _userService = userService;
            this.logger = logger;
            _classService = classService;
        }

        [AllowAnonymous]
        [HttpPatch("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model) {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll() { 
            var users = _userService.GetAll();
            return Ok(users);
        
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login(AuthenticateRequest request) {
            var response = _userService.Authenticate(request);
            
            return Ok(response);
        }


       

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // only admins can access other user recordss
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.UserId && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
            return Ok(user);
        }



        [Authorize]
        [HttpGet("classes")]
        public IActionResult GetById()
        {
            var currentUser = (User)HttpContext.Items["User"];
            List<ClassDTO> clss = _classService.getClassesOfUser(currentUser);
            return Ok(clss);
        }
        //[Authorize]
        [Authorize]
        [HttpGet("class/students/{id}")]
        public IActionResult ViewStudents(string id)
        {
            int cid;
            Int32.TryParse(id, out cid);
            List<User> students = _classService.getUsersEnrolledInClass(cid);
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (User user in students)
            {
                userDTOs.Add(new UserDTO().toDTO(user));
            }



            return Ok(userDTOs);
        }


        [HttpGet("Test")]
        public IActionResult Test() 
        {
            _userService.Test();
            return Ok();
        }

        [Authorize]
        [HttpPut("add/class/{code}")]
        public IActionResult addToClass(string code) 
        {
            User user = HttpContext.Items["User"] as User;
            ClassDTO _class = _classService.addUserToClass(user);



            return Ok(_class);
        }

        
    }
}
