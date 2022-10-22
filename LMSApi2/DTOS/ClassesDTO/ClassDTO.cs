using LMSApi2.Models;
using LMSApi2.DTOS.Instructors;
using LMSApi2.DTOS.Courses;

namespace LMSApi2.DTOS.ClassesDTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public InstrutorDTO instructor { get; set; }
        public string? Section { get; set; }
        
        public CourseDTO course { get; set; }

        public DateTime startDate { get; set; }


        public ClassDTO(Classes _class){
            Id = _class.ClassId;
            instructor = new InstrutorDTO(_class.Instructor);
            Section = _class.Section;
            course = new CourseDTO(_class.Course);
            startDate = _class.StartDate;
        }

    }
}
