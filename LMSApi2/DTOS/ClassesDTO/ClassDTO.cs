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

        public string? ClassCode { get; set; }

        public ClassDTO(Classes _class){
            Id = _class.ClassId;
            if (_class.Instructor != null) {
                instructor = new InstrutorDTO(_class.Instructor);

            }
            Section = _class.Section;
            if (_class.Course != null)
            {
                course = new CourseDTO(_class.Course);

            }
            startDate = _class.StartDate;
            ClassCode = _class.ClassCode;
        }
        public ClassDTO() { }

    }
}
