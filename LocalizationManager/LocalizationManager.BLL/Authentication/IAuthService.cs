using LocalizationManager.BLL.Model;

namespace LocalizationManager.BLL.Authentication;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto model);
    Task<string> LoginAsync(LoginDto model);
    Task LogoutAsync();
}
