using LMSApi2.Helpers;
using LMSApi2.Models;

using Microsoft.Extensions.Options;

using System.IO;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.FileUploadService
{
    public class FileUploadService :IFileUploadService
    {
        private readonly DataContext dataContext;
        private readonly IOptions<AppSettings> options;
        public FileUploadService(DataContext _context , IOptions<AppSettings> settings) {

            dataContext = _context;
            options = settings;

        
        }

        public async Task<bool> uploadFile(int announcementId , IFormFile file) {

            try
            {
                Console.WriteLine(Path.GetFullPath(options.Value.SaveFolderPath));
                string rootPath = Directory.GetCurrentDirectory();
                string savePath = rootPath + @"\" + options.Value.SaveFolderPath;
                string finalFilePath = savePath + @"\" + file.FileName;
                Console.WriteLine(Directory.GetCurrentDirectory());
                if (!Directory.Exists(Path.GetFullPath(options.Value.SaveFolderPath)))
                {

                    Directory.CreateDirectory(savePath);
                }
                using (FileStream fs = File.Create(finalFilePath))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                    
                }

                //save to database
                Announcement announcement =  await dataContext.Announcements.Where(el => (el.AnnouncementId == announcementId)).Include("AnnouncementFiles").FirstOrDefaultAsync();
                if (announcement == null) {
                    Console.WriteLine("null");

                }
                Console.Write(announcement.AnnouncementFiles.Count);
                announcement.AnnouncementFiles.Add(new AnnouncementFile() { 
                   FileName = file.FileName,
                   MimeType=  file.ContentType,
                   FilePath = finalFilePath,
                  
                });
                dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                throw new APIError(ex.Message);
                
            }



        }

        /* public async Task<bool> UploadService( SubmissionFile obj, IWebHostEnvironment Environment)
         {
             *//*if (!Directory.Exists(Environment.WebRootPath + "\\Files\\"))
             {
                 Directory.CreateDirectory(Environment.WebRootPath + "\\Files\\");

             }
             using (FileStream fs = System.IO.File.Create(Environment.WebRootPath + "\\Files\\" + obj.files.FileName))
             {
                 obj.files.CopyTo(fs);
                 fs.Flush();
                 return true;
             }
             return false;*//*
             await return false;
         }*/

    }
}
