using LMSApi2.Authorization;
using LMSApi2.DTOS;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.DTOS.Instructors;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.Teachers
{
    public class InstructorService : IInstructorService
    {
        private readonly DataContext _dataContext;
        private readonly IJwtUtils _jwtUtils;
        
        public InstructorService(DataContext context , IJwtUtils jwtUtils ) {
            _dataContext = context;
            _jwtUtils = jwtUtils;
        
        }
        public Instructor addInstructor(Instructor instructor)
        {
            _dataContext.Instructor.Add(instructor);
            _dataContext.SaveChanges();


            return instructor;
        }

        public AuthenticateResponseInstructor AuthenticateLogin(AuthenticateRequestInstructor request)
        {
            var _instructor = _dataContext.Instructor.Where(ins => (ins.UserName == request.username)).FirstOrDefault();
            // validate
            if (_instructor == null || _instructor.PasswordHash != request.password)
                throw new NotFoundException(ErrorMessages.dict[ERROR_TYPES.WRONG_CREDENTIALS]);

            // authentication successful so generate jwt token
            
            var jwtToken = _jwtUtils.GenerateJWTTokenTeacher(_instructor);

            return new AuthenticateResponseInstructor(_instructor,jwtToken);
        }


        
        public List<ClassDTO> Test()
        {
           
            List<Classes> cu = _dataContext.Users.Where(el => el.UserId == "k190146")
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

        public Instructor getInstructorById(string userId) {

            return _dataContext.Instructor.Find(userId);


        }
       
    }
}
