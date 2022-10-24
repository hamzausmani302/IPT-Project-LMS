using LMSApi2.DTOS.Courses;
using LMSApi2.DTOS.Instructors;

namespace LMSApi2.DTOS.ClassesDTO
{
    public class AddClassDTO
    {
        public string? instructorID { get; set; }
        public string? Section { get; set; }

        public string? courseID { get; set; }

        public DateTime startDate { get; set; }

    }
}
