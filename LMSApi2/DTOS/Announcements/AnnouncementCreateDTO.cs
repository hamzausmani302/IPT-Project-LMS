using LMSApi2.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMSApi2.DTOS.Announcements
{
    public class AnnouncementCreateDTO
    {



        public string? Description { get; set; } = "No Description";
        [Required]
        public string? Title { get; set; }


       
        public AnnouncementType announcementType { get; set; } = AnnouncementType.ANNOUNCEMENT;

        [AllowNull]
        public DateTime DueDate { get; set; }


    }
}
