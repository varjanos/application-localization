using LocalizationClient.Model;
using LocalizationClient.Services;

namespace LocalizationClient.ViewModels
{
    public class RegisterViewModel
    {
        public UserService userService = UserService.Instance();
        public User userToRegister = new User();
        public bool RegisterService(User user)
        {
            if (userService.Register(user))
            {
                return true;
            }
            return false;
        }
    }
}
