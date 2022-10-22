using LMSApi2.Authorization;
using LMSApi2.DTOS;
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


        
        public List<SubmissionFile> Test()
        {
            List<ClassesUser> cu = _dataContext.ClassesUsers.Where(el => 1==1).ToList() ;
            

           /* _dataContext.Instructor.Add(new Instructor() { Id = "t002", FacultyType = FacultyType.Visiting, Name = "hamza", PasswordHash = "password", UserName = "username" });

            _dataContext.SaveChanges();*/
            return new List<SubmissionFile>();
            

        }

        public Instructor getInstructorById(string userId) {

            return _dataContext.Instructor.Find(userId);


        }
        /*  public IEnumerable<Instructor> GetInstructors()
          {
              return _dataContext.Instructors;
          }
  */
        /* 

         public List<UserClassModel> GetAllClasses(string userId) {

             DbSet<User> users = _dataContext.Users;
             DbSet<StudentClasses> studentClasses = _dataContext.studentsClasses;
             List<UserClassModel> dat = new List<UserClassModel>();


             var m = _dataContext.studentsClasses.Join(
                 _dataContext.Users,

                 sc => sc.StudentId,
                 u => u.UserId,
                 (sc,user) =>new {user=user,sc=sc.Classes}




                 ).ToList();
             Console.WriteLine(m.Count);
             foreach (var _m in m) {
                 dat.Add(new UserClassModel() { user=_m.user , myClass=_m.sc });
             }
             return dat;
         }*/
    }
}
