using LocalizationClient.Model;
using LocalizationClient.Services;

namespace LocalizationClient.ViewModels
{
    public class LoginViewModel
    {
        public UserService userService = UserService.Instance();
        public User userToLogin = new User();

        public bool LoginService(User user)
        {
            if (userService.Login(user))
            {
                return true;
            }
            return false;
        }
    }
}
