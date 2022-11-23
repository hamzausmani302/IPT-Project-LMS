using LMSApi2.Authorization;
using LMSApi2.DTOS;
using LMSApi2.DTOS.Announcements;
using LMSApi2.DTOS.ClassesDTO;
using LMSApi2.DTOS.Instructors;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSApi2.Services.Teachers
{
    public class InstructorService : IInstructorService
    {
        private readonly DataContext _dataContext;
        private readonly IJwtUtils _jwtUtils;
        
        public InstructorService(DataContext context , IJwtUtils jwtUtils ) {
            _dataContext = context;
            _jwtUtils = jwtUtils;
        
        }
        public Instructor addInstructor(Instructor instructor)
        {
            _dataContext.Instructor.Add(instructor);
            _dataContext.SaveChanges();


            return instructor;
        }

        public AuthenticateResponseInstructor AuthenticateLogin(AuthenticateRequestInstructor request)
        {
            var _instructor = _dataContext.Instructor.Where(ins => (ins.UserName == request.username)).FirstOrDefault();
            // validate
            if (_instructor == null || _instructor.PasswordHash != request.password)
                throw new NotFoundException(ErrorMessages.dict[ERROR_TYPES.WRONG_CREDENTIALS]);

            // authentication successful so generate jwt token
            
            var jwtToken = _jwtUtils.GenerateJWTTokenTeacher(_instructor);

            return new AuthenticateResponseInstructor(_instructor,jwtToken);
        }


        
        public List<ClassDTO> Test()
        {
           
            List<Classes> cu = _dataContext.Users.Where(el => el.UserId == "k190146")
                .Include("Classes")
                .Include("Classes.Instructor")
                .Include("Classes.Course")
                .First()
                .Classes;

            
            List<ClassDTO> classDTOs = new List<ClassDTO>();
            foreach (Classes cls in cu)
            {
                classDTOs.Add(new ClassDTO(cls));
            }

      
            return classDTOs;
            

        }

        public Instructor getInstructorById(string userId) {

            return _dataContext.Instructor.Find(userId);


        }
        public AnnouncementResponse addAnnouncementInClass(int classId , AnnouncementCreateDTO dto) {
            
                Classes _class = _dataContext._Classes.Where(cl => (cl.ClassId == classId)).Include("Announcements").First();
            Console.WriteLine(_class.CourseID);
                Announcement announcement = new Announcement()
                {
                    Classes = _class,
                    announcementType = dto.announcementType,
                    CreatedAt = DateTime.Now,
                    Description = dto.Description,
                    DueDate = dto.DueDate,
                    Title = dto.Title
                };
            try { 
                _dataContext.Announcements.Add(announcement);
                _dataContext.SaveChanges();
                return new AnnouncementResponse(announcement);
            }
            catch (Exception ex) {
                throw new APIError("Unable to save data" + ex.StackTrace); 
            
            }
            
        
        }

        public async Task<List<AnnouncementResponse>> getAllAssignmentsOfATeacher(Instructor instructor) {
            List<Classes> teacherClasses =  await _dataContext._Classes.Where(el => el.InstructorId == instructor.Id).Include("Announcements").Include("Course").ToListAsync();
            List<AnnouncementResponse> announcements = new List<AnnouncementResponse>();
            foreach (Classes teacherClass in teacherClasses) {
                foreach (Announcement announcement in _dataContext.Announcements) {
                    if (announcement.announcementType == AnnouncementType.ASSIGNMENT) {
                        announcements.Add(new AnnouncementResponse(announcement));
                    }
                    
                }
            }


            return announcements;
        
        }

        public async  Task<List<SubmissionFile>> getSubmissionOfStudents(int announcementId) {
            List<SubmissionFile> submissionFiles = await  _dataContext.SubmissionFile.Where(el => el.AnnouncementId == announcementId).ToListAsync();
            return submissionFiles;

        }
    }
}
