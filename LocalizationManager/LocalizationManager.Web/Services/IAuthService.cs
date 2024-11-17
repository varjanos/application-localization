using LocalizationManager.Transfer.AuthDtos;

namespace LocalizationManager.Web.Services;


public interface IAuthService
{
    Task LoginAsync(LoginDto loginModel);

    Task RegisterAsync(RegisterDto registerModel);

    Task LogoutAsync();
}
