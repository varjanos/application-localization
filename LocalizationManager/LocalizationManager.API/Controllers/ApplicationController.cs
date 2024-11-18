using LocalizationManager.BLL.Application;
using LocalizationManager.Transfer.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ApplicationController(IApplicationManagingService _applicationManagingService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ApplicationDto>>> GetRegisteredApplications()
    {
        var applications = await _applicationManagingService.GetRegisteredApplicationsAsync();
        return Ok(applications);
    }

    [HttpPost]
    public async Task<ActionResult> RegisterApplication([FromBody] ApplicationDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Application data is required.");
        }

        await _applicationManagingService.RegisterApplicationAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateApplication([FromBody] ApplicationDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Application data is required.");
        }

        await _applicationManagingService.UpdateApplicationAsync(dto);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteApplication([FromBody] ApplicationDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Application data is required.");
        }

        await _applicationManagingService.DeleteApplicationAsync(dto);
        return Ok();
    }
}
