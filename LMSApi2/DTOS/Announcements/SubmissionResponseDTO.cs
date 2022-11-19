using LMSApi2.Models;

namespace LMSApi2.DTOS.Announcements
{
    public class SubmissionResponseDTO
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int AnnouncementId { get; set; }
        public string StudentId { get; set; }
        public SubmissionResponseDTO(SubmissionFile file) {
            this.ID = file.Id;
            this.FileName = file.FileName;
            this.MimeType = file.MimeType;
            this.AnnouncementId = file.AnnouncementId;
            this.StudentId = file.StudentId;
            
        }
    }
}
