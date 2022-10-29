using LMSApi2.Models;

namespace LMSApi2.Services.FileUploadService
{
    public interface IFileUploadService
    {
        public  Task<bool> uploadFile(int announcementId , IFormFile file);

        public Task<AnnouncementFile> getFile(string filename);
    }
}
