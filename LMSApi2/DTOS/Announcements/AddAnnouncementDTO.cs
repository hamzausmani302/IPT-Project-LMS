using LMS.DTOS.FileDto;

namespace LMS.DTOS.Announcements
{
    public class AddAnnouncementDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public string announcementType { get; set; }

        public string dueDate { get; set; }

        public List<FileDTO> attachedFiles { get; set; }
    }
}
