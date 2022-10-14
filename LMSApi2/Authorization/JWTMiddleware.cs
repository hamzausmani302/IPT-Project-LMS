using LMSApi2.Helpers;
using LMSApi2.Services.Users;
using Microsoft.Extensions.Options;
using LMSApi2.Models;

namespace LMSApi2.Authorization
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;
        private readonly ILogger<User> logger1;
        public JWTMiddleware(RequestDelegate next,IOptions<AppSettings> settings, ILogger<User> logger)
        {
            _next = next;
            logger1 = logger;
            this.appSettings = settings.Value ;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils) {
            
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ;
            var userId = jwtUtils.ValidateJwtToken(token);
            logger1.LogInformation(token);
            logger1.LogInformation(userId);

            if (userId != null)
            {
                // attach user to context on successful jwt validation
                //logger1.LogInformation(userId.Value.ToString());
                context.Items["User"] = userService.GetById(userId);
            }

            await _next(context);

        }

    }
}
