using LMSApi2.Helpers;
using LMSApi2.Models;

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

        public IEnumerable<Instructor> GetInstructors()
        {
            return _dataContext.Instructors;
        }
    }
}
