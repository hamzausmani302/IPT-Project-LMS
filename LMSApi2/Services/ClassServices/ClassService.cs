using LMSApi2.DTOS.Announcements;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.ClassServices
{
    public class ClassService : IClassService
    {

        private readonly DataContext _context;
        public ClassService(DataContext context) {
            _context = context;

        }
        public List<ClassDTO> getClassesOfUser(User user)
        {
            List<Classes> cu = _context.Users.Where(el => el.UserId == user.UserId)
                 .Include("Classes")
                 .Include("Classes.Instructor")
                 .Include("Classes.Course")
                 .First()
                 .Classes;


            List<ClassDTO> classDTOs = new List<ClassDTO>();
            foreach (Classes cls in cu)
            {
                classDTOs.Add(new ClassDTO(cls));
            }


            return classDTOs;


        }

        public List<User> getUsersEnrolledInClass(int classId) {

            try
            {
                Classes _class = _context._Classes.Where(el => (el.ClassId == classId)).Include("Users").First();
                if (_class == null) { throw new NotFoundException(); }
                List<User> enrolledStudents = _class.Users;
               


                return enrolledStudents;
            }
            catch (Exception err) {
                Console.WriteLine(err.Message);
                throw new NotFoundException();
            }
        }

        public List<ClassDTO> getClassesOfInstructor(Instructor instructor)
        {
            List<Classes> _classList = _context._Classes.Where(el => (instructor.Id == el.InstructorId)).Include("Course").Include("Instructor").ToList();
            if (_classList.Count == 0)
            {
                Console.WriteLine("no classes exists");
                return new List<ClassDTO>();

            }
            List<ClassDTO> classDTOs = new List<ClassDTO>();
            foreach (Classes cls in _classList)
            {
                classDTOs.Add(new ClassDTO(cls));
            }

            return classDTOs;

        }



        public async Task<ClassDTO> addUserToClass(string code , User user) {

            

            Classes _class = await _context._Classes.Where(el => (el.ClassCode == code))
            .Include("Users")
            .Include("Instructor")
            .Include("Course")
            .FirstOrDefaultAsync();
            //Console.WriteLine(_class.Users.Count);
            if (_class == null)
            {
                throw new NotFoundException("No such class exists");
            }
            if (_class.Users.Contains(user)) {
                throw new APIError("Duplicate Entry. Already enrolled");
            }

               
                ClassDTO dto = new ClassDTO(_class);
                
                _class.Users.Add(user);
                _context.SaveChanges();
                return dto; 
        
        
          
        }

        public Classes addANewClass(AddClassDTO dto , Instructor instructor) {
            try
            {
                string randomCode = UtilFunctions.generateClassCode();
                Course course = _context.Courses.Find(dto.courseID);
                if (course == null) {
                    throw new NotFoundException("Course not found");
                }
                Classes _class = new Classes()
                {
                    Instructor = instructor,
                    Course = _context.Courses.Find(dto.courseID),
                    StartDate = dto.startDate,
                    Section = dto.Section,
                    ClassCode = randomCode

                };
                _context._Classes.Add(_class);
                _context.SaveChanges();
                return _class;
            }
            catch (DbUpdateException exption) {
                throw new APIError(exption.Message);
            }
            


          

            
        }

        public bool isClassExists(int classId) { 
            Classes? _class = _context._Classes.Find(classId);
            if (_class != null) {
                return true;
            }
            return false;

        }


        public List<AnnouncementResponse> viewAnnoucements(int id) { 
            
            List<Announcement> annoucements = _context.Announcements.Where(el=>el.ClassesId == id).Include("AnnouncementFiles")
                .Include("Classes")
                .Include("Classes.Instructor")
                .Include("Classes.Course")
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
            List<AnnouncementResponse> announcementDTOs = new List<AnnouncementResponse>();
            foreach (var annoucement in annoucements) {
                announcementDTOs.Add(new AnnouncementResponse(annoucement));
            }
            return announcementDTOs;
        }
    }
}
