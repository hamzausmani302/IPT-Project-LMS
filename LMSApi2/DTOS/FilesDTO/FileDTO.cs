using System.Text.Json.Serialization;

namespace LMS.DTOS.FileDto
{
    public class FileDTO
    {
        public string? FileName { get; set; }
        public byte[]? Data { get; set; }
        public string? MimeType { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
