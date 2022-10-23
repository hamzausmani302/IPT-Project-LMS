using LMSApi2.Authorization;
using LMSApi2.DTOS.Users;
using LMSApi2.Helpers;
using LMSApi2.Models;
using Microsoft.EntityFrameworkCore;
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
            var user = _context.Users.Where(el => model.Username == el.UserId).FirstOrDefault();

            // validates
            if (user == null || user.PasswordHash != model.Password)
                throw new NotFoundException(ErrorMessages.dict[ERROR_TYPES.WRONG_CREDENTIALS]);

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return  _context.Users.Where(el => (1==1));
        }

        public User GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public void Test() {
            var users = _context.Users.Where(el => (el.UserId == "k190220")).Include("Classes").First();
            var _class = _context._Classes.Find(2);
            users.Classes.Add(_class);
            _context.SaveChanges();
            
           
        }
    }
}
