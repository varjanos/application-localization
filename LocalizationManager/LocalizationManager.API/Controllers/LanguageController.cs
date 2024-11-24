using Microsoft.AspNetCore.Mvc;
using LocalizationManager.Transfer.LocalizationDtos;
using LocalizationManager.BLL.Language;
using Microsoft.AspNetCore.Authorization;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LanguageController(ILanguageService _languageService) : ControllerBase
{
    [HttpGet("all-languages")]
    public async Task<List<LanguageDto>> GetAllLanguagesAsync()
        => await _languageService.GetAllLanguagesAsync();

    [HttpGet("supported-languages")]
    public async Task<List<LanguageDto>> GetSupportedLanguagesForApplication(int applicationId)
    => await _languageService.GetSupportedLanguagesForApplicationAsync(applicationId);
}
