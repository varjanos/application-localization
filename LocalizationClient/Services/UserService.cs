using LocalizationClient.Model;
using Microsoft.AspNetCore.Identity;
namespace LocalizationClient.Services
{
    public class UserService
    {
        private static UserService _instance;
        public static List<User> users = new List<User>();
        public static User? loggedInUser { get; set; }
        private UserService(){}
        public static UserService Instance()
        {
            if (_instance == null)
            {
                _instance = new UserService();
            }
            return _instance;
        }

        public bool Register(User userToRegister)
        {
            foreach (var user in users)
            {
                if (user.Username == userToRegister.Username)
                {
                    return false;
                }
            }
            users.Add(userToRegister);
            return true;
        }
        public bool Login(User user)
        {
            foreach (var users in users)
            {
                if (users.Username == user.Username)
                {
                    loggedInUser = user;
                    return true;
                }
            }
            return false;
        }
    }
   
}
