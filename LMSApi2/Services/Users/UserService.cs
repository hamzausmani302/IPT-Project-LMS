using LMSApi2.Authorization;
using LMSApi2.DTOS.Users;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.Extensions.Options;

namespace LMSApi2.Services.Users
{

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IOptions<AppSettings> settings;


        public UserService(DataContext context , IJwtUtils jwtutils , IOptions<AppSettings> settings) {

            _context = context;
            _jwtUtils = jwtutils;
            this.settings = settings;
        }
        
        
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Username);

            // validate
            if (user == null || user.PasswordHash != "password")
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return  _context.Users;
        }

        public User GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
