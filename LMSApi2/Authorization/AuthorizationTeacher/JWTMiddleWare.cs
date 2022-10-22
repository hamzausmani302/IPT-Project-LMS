using LMSApi2.Helpers;
using LMSApi2.Models;
using LMSApi2.Services.Teachers;
using LMSApi2.Services.Users;
using Microsoft.Extensions.Options;

namespace LMSApi2.Authorization.AuthorizationTeacher
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;
        private readonly ILogger<User> logger1;
        public JWTMiddleWare(RequestDelegate next, IOptions<AppSettings> settings, ILogger<User> logger)
        {
            _next = next;
            logger1 = logger;
            appSettings = settings.Value;
        }
        public async Task Invoke(HttpContext context, IInstructorService instuctorService, IJwtUtils jwtUtils)
        {
            logger1.LogInformation("teacher middleware");
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtTokenTeacher(token);
            logger1.LogInformation(token);
            //logger1.LogInformation(userId);

            if (userId != null)
            {
                // attach user to context on successful jwt validation
                //logger1.LogInformation(userId.Value.ToString());
                Instructor instructor = instuctorService.getInstructorById(userId);
                context.Items["Instructor"] = instructor;

            

            }

            await _next(context);

        }
    }
}
