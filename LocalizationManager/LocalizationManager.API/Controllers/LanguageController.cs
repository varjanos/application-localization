using Microsoft.AspNetCore.Mvc;
using LocalizationManager.BLL.Model;
using LocalizationManager.BLL.Language;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguageController(ILanguageService _languageService) : ControllerBase
{
    [HttpGet]
    public async Task<List<LanguageDto>> GetAllLanguagesAsync()
        => await _languageService.GetAllLanguagesAsync();
}
