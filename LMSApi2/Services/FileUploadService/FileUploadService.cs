using LMSApi2.Helpers;
using LMSApi2.Models;

using Microsoft.Extensions.Options;

using System.IO;
using Microsoft.EntityFrameworkCore;
using LMS.DTOS.FileDto;

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
                string savePath = rootPath + @"/" + options.Value.SaveFolderPath;
                string finalFilePath = savePath + @"/" + file.FileName;
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
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.StackTrace);
                throw new APIError(ex.Message);
                
            }



        }


        public async Task<bool> uploadFile1(int announcementId, FileDTO file)
        {

            try
            {
                Console.WriteLine(Path.GetFullPath(options.Value.SaveFolderPath));
                string rootPath = Directory.GetCurrentDirectory();
                string savePath = rootPath + @"/" + options.Value.SaveFolderPath;
                string finalFilePath = savePath + @"/" + file.FileName;
                Console.WriteLine(Directory.GetCurrentDirectory());
                if (!Directory.Exists(Path.GetFullPath(options.Value.SaveFolderPath)))
                {

                    Directory.CreateDirectory(savePath);
                }
                MemoryStream stream = new MemoryStream(file.Data);

                using (FileStream fs = File.Create(finalFilePath))
                {
                    await stream.CopyToAsync(fs);
                    fs.Flush();

                }

                //save to database
                Announcement announcement = await dataContext.Announcements.Where(el => (el.AnnouncementId == announcementId)).Include("AnnouncementFiles").FirstOrDefaultAsync();
                if (announcement == null)
                {
                    Console.WriteLine("null");

                }
                Console.Write(announcement.AnnouncementFiles.Count);
                announcement.AnnouncementFiles.Add(new AnnouncementFile()
                {
                    FileName = file.FileName,
                    MimeType = file.MimeType,
                    FilePath = finalFilePath,

                });
                dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.StackTrace);
                throw new APIError(ex.Message);

            }



        }


        public Task<AnnouncementFile> getFile(string filename) {
            var file = dataContext.AnnouncementFile.Where(el => el.FileName == filename).FirstAsync();
            return file;
        }


       

        public async  Task uploadSubmissionFile(int announcementId, IFormFile file , User user) {
            Announcement currentAnnouncement =await dataContext.Announcements.FindAsync(announcementId);
            if (currentAnnouncement == null) {
                throw new NotFoundException("No such announcement exists");
            }

            string rootPath = Directory.GetCurrentDirectory();
            string savePath = rootPath + @"/" + options.Value.SaveFolderPath;
            string finalFilePath = savePath + @"/" + file.FileName;

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
                SubmissionFile fileToSubmit = new SubmissionFile()
                {
                    FileName = file.FileName,
                    FilePath = finalFilePath,
                    MimeType = file.ContentType,
                    Announcement = currentAnnouncement,
                    User = user

                };
                await dataContext.SubmissionFile.AddAsync(fileToSubmit);
                await dataContext.SaveChangesAsync();
                    
        
           

        }

    }
}
