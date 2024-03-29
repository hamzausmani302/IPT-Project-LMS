﻿using LMS.DTOS.FileDto;
using LMSApi2.DTOS.Announcements;
using LMSApi2.DTOS.Users;
using LMSApi2.Models;

namespace LMSApi2.Services.Users
{
    public interface IUserService

    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(string id);

        public Task<List<SubmissionResponseDTO>> GetAllAssignmentFilesOfAUser(int announcementId , User user);

        public Task<Announcement> isAnnouncementExists(int announcementId);

        public bool isUserInClass(int classId , User user);


        public void Test();

        
    }
}
