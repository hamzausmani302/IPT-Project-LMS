using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LMSApi2.Models
{

    public enum AnnouncementType { 
        ASSIGNMENT,
        ANNOUNCEMENT
    }

    [Table("Announcements")]
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }
        [Required]
        public int ClassId { get; set; }
        public virtual Classes Classes { get; set; }        //Navigation attributes

        [AllowNull]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public AnnouncementType announcementType { get; set; }

        public DateTime DueDate { get; set; }

        public virtual List<AnnouncementFile>? AnnouncementFiles { get; set; }

        public virtual List<SubmissionFile>? SubmissionFiles { get; set; }







    }
}
