using LMS.DTOS.FileDto;
using LMSApi2.Models;

namespace LMSApi2.Services.FileUploadService
{
    public interface IFileUploadService
    {
        public  Task<bool> uploadFile(int announcementId , IFormFile file);
        public Task<bool> uploadFile1(int announcementId, FileDTO file);


        public Task uploadSubmissionFile(int announcementId , IFormFile file , User user);

        public Task<AnnouncementFile> getFile(string filename);
    }
}
