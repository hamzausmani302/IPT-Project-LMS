﻿using LMS.DTOS.FileDto;
using LMSApi2.Models;

namespace LMSApi2.Services.FileUploadService
{
    public interface IFileUploadService
    {
        public  Task<bool> uploadFile(int announcementId , IFormFile file);
        public Task<bool> uploadFile1(int announcementId, FileDTO file);


        public Task uploadSubmissionFile(int announcementId ,FileDTO file , User user);

        public Task<AnnouncementFile> getFile(string filename);

        public Task<SubmissionFile> retrieveFileDataFromDB(int id);

        public Task<FileDTO> retrieveFile(SubmissionFile file);
        public Task<FileDTO> retrieveFile(AnnouncementFile file);
        public Task<AnnouncementFile> retrieveAnnouncementFileInfo(int id);

        

    }
}
