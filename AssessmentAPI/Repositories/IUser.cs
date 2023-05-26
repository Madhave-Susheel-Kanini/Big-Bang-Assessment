using AssessmentAPI.Model;

namespace AssessmentAPI.Repositories
{
    public interface IUser
    {
        public IEnumerable<User> GetUser();
        public User GetUserById(int UserId);
        public User PostUser(User user);
        public User PutUser(int UserId, User user);
        public User DeleteUser(int UserId);

    }
}
