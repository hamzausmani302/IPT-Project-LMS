namespace LMSApi2.Models
{
    public class Instructor_Course
    {
        public int Id { get; set; }
        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
