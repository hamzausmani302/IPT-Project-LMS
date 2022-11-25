using LMSApi2.Helpers;
using LMSApi2.Models;

using Microsoft.Extensions.Options;

using System.IO;
using Microsoft.EntityFrameworkCore;
using LMS.DTOS.FileDto;
using Azure.Storage.Blobs;

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
                MemoryStream stream = new MemoryStream();
                await file.CopyToAsync(stream);
                FileDTO fileDTO = new FileDTO() { Data =stream.ToArray() , FileName = file.FileName , MimeType = file.ContentType};
                string remoteName = getFileName(fileDTO);
                Console.WriteLine("error here 1" + file.FileName);
                try
                {
                    await uploadFileToAzure(fileDTO, remoteName);
                }
                catch (Exception)
                {
                    throw new APIError("Failed to upload file");
                }


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
                DateTime dateTime = DateTime.Now;
                string remoteName = getFileName(file , dateTime);
                Console.WriteLine("error here 1" + file.FileName);
                try
                {
                    await uploadFileToAzure(file, remoteName);
                }
                catch (Exception)
                {
                    throw new APIError("Failed to upload file");
                }


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
                    throw new NotFoundException("announcement not found");

                }
                Console.Write(announcement.AnnouncementFiles.Count);
                announcement.AnnouncementFiles.Add(new AnnouncementFile()
                {
                    FileName = file.FileName,
                    MimeType = file.MimeType,
                    FilePath = finalFilePath,
                    CreatedAt =   dateTime

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

        private string getFileName(FileDTO file ) {

            return $"{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString()}_{file.FileName}";
        }

        private string getFileName(FileDTO file , DateTime createdAt)
        {

            return $"{new DateTimeOffset(createdAt).ToUnixTimeMilliseconds().ToString()}_{file.FileName}";
        }
        private string getAssignmentFileName(string remoteName) {
            string? name  = remoteName.Replace("/", "").Replace(@"\" , "");
            string path = Path.Combine("submissions", name);
            return path;
        }
        private async Task<FileDTO> retrieveFileFromAzure(AnnouncementFile file)
        {
            FileDTO dto = new FileDTO() {FileName= file.FileName , MimeType = file.MimeType , CreatedAt = file.CreatedAt };
            string connectionString = options.Value.AzureWebJobsStorage;
            string ContainerName = options.Value.ContainerName;
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            //Console.WriteLine(file.remoteName);
            Console.WriteLine("log-name" + getFileName(dto, file.CreatedAt));
            BlobClient blobClient = containerClient.GetBlobClient(getFileName(dto , file.CreatedAt));
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                MemoryStream stream = new MemoryStream();
                await response.Value.Content.CopyToAsync(stream);

                FileDTO fileDTO = new FileDTO() { FileName = file.FileName, MimeType = file.MimeType, Data = stream.ToArray() };
                return fileDTO;
            }
            else
            {
                throw new APIError("Server Error - no such file exists");
            }
        }


        private async Task<FileDTO> retrieveFileFromAzure(SubmissionFile file) {
            string connectionString = options.Value.AzureWebJobsStorage;
            string ContainerName = options.Value.ContainerName;
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            //Console.WriteLine(file.remoteName);
            Console.WriteLine(getAssignmentFileName(file.remoteName));
            BlobClient blobClient = containerClient.GetBlobClient(getAssignmentFileName(file.remoteName));
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                MemoryStream stream = new MemoryStream();
                await response.Value.Content.CopyToAsync(stream);

                FileDTO fileDTO = new FileDTO() { FileName = file.FileName, MimeType = file.MimeType, Data = stream.ToArray() };
                return fileDTO;
            }
            else {
                throw new APIError("Server Error");
            }
        }

        public async Task uploadFileToAzure(FileDTO file , string remoteName , bool isAssignment=false) {

          
                string connectionString = options.Value.AzureWebJobsStorage;
                string ContainerName = options.Value.ContainerName;
                BlobContainerClient container = new BlobContainerClient(connectionString, ContainerName);


            string saveFileName = remoteName;
            if (isAssignment) { 
                saveFileName = getAssignmentFileName(remoteName);
            }

            BlobClient client = container.GetBlobClient(saveFileName);

            MemoryStream stream = new MemoryStream(file.Data);
            await client.UploadAsync(stream);
                
           
        }
        
       

        public async  Task uploadSubmissionFile(int announcementId, FileDTO file , User user) {
            Console.WriteLine("test inaside");
             Announcement currentAnnouncement =await dataContext.Announcements.FindAsync(announcementId);
            if (currentAnnouncement == null) {
                throw new NotFoundException("No such announcement exists");
            }
          /*  if (DateTime.Compare(DateTime.Now, currentAnnouncement.DueDate) == 1)
            {
                throw new ValidationException("Date passed already");
            }*/

            string remoteName = getFileName(file);
            bool isAssignment = true;
            try
            {
                await uploadFileToAzure(file, remoteName ,isAssignment);
            }
            catch (Exception)
            {
                throw new APIError("Failed to upload file");
            }
            /*     Console.WriteLine("error here 2");*/
            string rootPath = Directory.GetCurrentDirectory();
            string savePath = rootPath + @"/" + options.Value.SaveFolderPath;
            string finalFilePath = savePath + @"/submissions/" + file.FileName;

            
            if (!Directory.Exists(Path.GetFullPath(options.Value.SaveFolderPath)))
            {

                Directory.CreateDirectory(savePath);
            }
            
                using (FileStream fs = File.Create(finalFilePath))
                {
                fs.Write(file.Data, 0, file.Data.Length);

                }
                SubmissionFile fileToSubmit = new SubmissionFile()
                {
                    FileName = file.FileName,
                    FilePath = finalFilePath,
                    MimeType = file.MimeType,
                    Announcement = currentAnnouncement,
                    User = user,
                    CreatedAt = DateTime.Now,
                    remoteName = remoteName
                };
                await dataContext.SubmissionFile.AddAsync(fileToSubmit);
                await dataContext.SaveChangesAsync();
                    
        
           

        }

        public async Task<SubmissionFile> retrieveFileDataFromDB(int fileId ) {
            SubmissionFile submissionFile =  await dataContext.SubmissionFile.FindAsync(fileId);
            if (submissionFile == null) {
                throw new NotFoundException("no such file exists");
            }
            return submissionFile;
        }

        public async Task<FileDTO> retrieveFile(SubmissionFile file) {

           FileDTO responseFile =  await retrieveFileFromAzure(file);
            if (responseFile == null) {
                throw new NotFoundException();
            }
            return responseFile;
        }


        public async Task<AnnouncementFile> retrieveAnnouncementFileInfo(int id) {
            AnnouncementFile announcementfile = await  dataContext.AnnouncementFile.Where(el => el.Id == id).Include("Announcement").Include("Announcement.Classes").FirstAsync();
            if (announcementfile == null) {
                throw new NotFoundException();
            }

            return announcementfile;
        }
        public async Task<FileDTO> retrieveFile(AnnouncementFile file) {
            FileDTO responseFile = await retrieveFileFromAzure(file);
            if (responseFile == null)
            {
                throw new NotFoundException();
            }
            return responseFile;
        }
    }
}
