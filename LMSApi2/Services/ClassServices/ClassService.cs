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



        public ClassDTO addUserToClass(User user) {
           
                Classes _class = _context._Classes.Where(el => (el.ClassId == 2))
                .Include("Users")
                .Include("Instructor")
                .Include("Course")
                .First();
                //Console.WriteLine(_class.Users.Count);

                if (_class == null)
                {
                    throw new NotFoundException("No such class exists");
                }
                ClassDTO dto = new ClassDTO(_class);
                Console.WriteLine(_class.CourseID);
                _class.Users.Add(user);
                _context.SaveChanges();
                return dto; 
        
        
          
        }

        public void addANewClass(AddClassDTO dto) {
            try
            {

                _context._Classes.Add(new Classes()
                {
                    InstructorId = dto.instructorID,
                    CourseID = dto.courseID,
                    StartDate = dto.startDate,
                    Section = dto.Section
                });
                _context.SaveChanges();
            }
            catch (DbUpdateException exption) {
                throw new APIError(exption.Message);
            }
            


          

            
        }
    }
}
