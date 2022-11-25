using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMSApi2.Models
{
    public class AnnouncementFile
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
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public virtual Announcement Announcement { get; set; }          //Navigation reference



    }
}
