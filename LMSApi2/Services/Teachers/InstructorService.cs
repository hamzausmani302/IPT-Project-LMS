using LMSApi2.DTOS;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.Teachers
{
    public class InstructorService : IInstructorService
    {
        private readonly DataContext _dataContext;
        
        public InstructorService(DataContext context) {
            _dataContext = context;    
        
        }
        public Instructor addInstructor(Instructor instructor)
        {
            _dataContext.Add(instructor);
            _dataContext.SaveChanges();


            return instructor;
        }

        public List<SubmissionFile> Test()
        {

            User user = _dataContext.Users.Where(el => (el.UserId == "k190146")).Include(b=>b.SubmissionFiles).First();
            //List<SubmissionFile> files = _dataContext.SubmissionFile.Where(f => f.StudentId == user.UserId).ToList();
            Console.WriteLine(user.SubmissionFiles.Count);
            return user.SubmissionFiles;
            
            

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
