using LMSApi2.Services.Teachers;

using Microsoft.AspNetCore.Mvc;
using LMSApi2.Models;

using LMSApi2.Authorization.AuthorizationTeacher;
using LMSApi2.DTOS.Instructors;
using LMSApi2.Services.ClassServices;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.DTOS.Users;
using LMSApi2.DTOS.Announcements;
using LMSApi2.Helpers;
using LMSApi2.Services.FileUploadService;
using Microsoft.IdentityModel.Tokens;

namespace LMSApi2.Controllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly IInstructorService _service;
        private readonly ILogger<Instructor> logger;
        private readonly IClassService _classService;
        private readonly IFileUploadService _uploadService;
        public TeacherController(IFileUploadService fileUploadService ,IInstructorService instructorService , IClassService classService , ILogger<Instructor> logger) {
            _service = instructorService;
            this.logger = logger;
            _classService = classService;
            _uploadService = fileUploadService;
        }

        [Authorize]
        [HttpGet("annoucements/class/{id}")]
        public IActionResult getAllAnnoucementsOfAClass(string id)
        {

            int.TryParse(id, out int cid);
            if (cid == 0 || cid == null || !_classService.isClassExists(cid))
            {
                throw new NotFoundException("no such class exists");
            }
            List<AnnouncementResponse> announcements = _classService.viewAnnoucements(cid);

            return Ok(announcements);
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

        [Authorize]
        [HttpPost("class/add")]
        public IActionResult createClass(AddClassDTO addClassDTO) {

            _classService.addANewClass(addClassDTO);
            return Ok("done");
        }

        
        /*[Authorize]*/
        [HttpPost("announcement/add/{id}")]
        public IActionResult addAnnouncement(string id , [FromBody] AnnouncementCreateDTO annoucementDTO) {

            int.TryParse(id, out int cid);
            if (cid == 0) {
                throw new APIError("no such class exists");
            }
            AnnouncementResponse announcement =  _service.addAnnouncementInClass(cid , annoucementDTO);

            return Ok(announcement);
        }

        [HttpGet("Test")]
        public IActionResult Test() {
            List<ClassDTO> clss = _service.Test();
            return Ok(clss);
        }


        [HttpPost("upload/class/{id}")]
        public async Task<IActionResult> uploadFile(string id , [FromForm]List<IFormFile> fileToUpload) {
            Console.WriteLine(HttpContext.Request.Form.Files.Count);
            Console.WriteLine(fileToUpload.Count);
            IFormCollection collection =  HttpContext.Request.Form;

            AnnouncementType announcementType = collection["announcementType"] == "ASSIGNMENT" ?  AnnouncementType.ASSIGNMENT : (collection["announcementType"] == "ANNOUNCEMENT" ? AnnouncementType.ANNOUNCEMENT : throw new APIError("Announcement type is incorrect") ) ;
            if (collection["title"].IsNullOrEmpty()) {
                throw new APIError("title is not provided");
            }
            if (collection["dueDate"].IsNullOrEmpty()) {
                throw new APIError("dueDate is not provided");

            }

            DateTime dueDate=  DateTime.Parse(collection["dueDate"] );
            if (dueDate == null) {
                throw new APIError("Invlaid datetime format");
            }
            AnnouncementCreateDTO annoucementDTO = new AnnouncementCreateDTO() { 
                announcementType = announcementType,
                Description= collection["description"],
                DueDate=dueDate,
                Title = collection["title"]




            };
            int.TryParse(id, out int cid);
            if (cid == 0)
            {
                throw new APIError("no such class exists");
            }
            Console.WriteLine("saving anbnouncemnt");
            AnnouncementResponse announcement = _service.addAnnouncementInClass(cid, annoucementDTO);
            if (announcement == null) {
                throw new APIError("Error creating announcement");
            }
            Console.WriteLine("created announcemnt" + announcement.AnnouncementId.ToString());
            int fileUploaded = 0;
            foreach (IFormFile file in fileToUpload) {
                bool result =await _uploadService.uploadFile(announcement.AnnouncementId , file);
                if (result == true) {
                    fileUploaded++;
                    
                }
            }




            //          Console.WriteLine(val);
            return Ok(new {Accouncement = announcement , Count= fileUploaded }); 
        }


    }
}
