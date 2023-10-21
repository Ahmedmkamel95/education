using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;

public class LevelsController : ApiControllerBase
{

    public LevelsController() { }

    [Authorize(Roles = $"{Role.Admin},{Role.Teacher}")]
    [HttpGet]
    public async Task<IActionResult> GetLevels()
    {
        return Ok(await Mediator.Send(new GetLevelsQuery()));
    }
}
