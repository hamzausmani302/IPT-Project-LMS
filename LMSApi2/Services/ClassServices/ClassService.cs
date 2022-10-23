using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.Helpers;
using LMSApi2.Models;
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


        public List<ClassDTO> getClassesOfInstructor(Instructor instructor)
        {
            List<Classes> _classList = _context._Classes.Where(el => (instructor.Id == el.InstructorId)).Include("Course").Include("Instructor").ToList();
            if (_classList.Count == 0) {
                Console.WriteLine("no classes exists");
                return new List<ClassDTO>();

            }
            List<ClassDTO> classDTOs = new List<ClassDTO>();
            foreach (Classes cls in _classList) {
                classDTOs.Add(new ClassDTO(cls));
            }

            return classDTOs;
           
        }
    }
}
