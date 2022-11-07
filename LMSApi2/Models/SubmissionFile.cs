using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMSApi2.Models
{
    public class SubmissionFile
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]

        public string MimeType { get; set; }
        [Required]

        public string FilePath { get; set; }

        public int AnnouncementId { get; set; }
        [JsonIgnore]
        public virtual Announcement Announcement { get; set; }          //Navigation reference

        [ForeignKey("User")]
        public string? StudentId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }




    }
}
