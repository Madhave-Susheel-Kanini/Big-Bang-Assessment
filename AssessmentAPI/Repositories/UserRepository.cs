using AssessmentAPI.Model;
using AssessmentAPI.Models;
using AssessmentAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories
{
    public class UserRepository : IUser
    {
        private readonly HotelContext _context;

        public UserRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int UserId)
        {
            return _context.Users.Find(UserId);
        }

        public User PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User PutUser(int UserId, User user)
        {
            var existingUser = _context.Users.Find(UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.UserEmail = user.UserEmail;
                existingUser.UserPassword = user.UserPassword;

                _context.SaveChanges();
            }
            return existingUser;
        }

        public User DeleteUser(int UserId)
        {
            var user = _context.Users.Find(UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }
    }
}
