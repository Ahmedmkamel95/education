using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Application.Queries.GetLevels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;

public class LevelsController : ApiControllerBase
{

    public LevelsController() { }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetLevels()
    {
        return Ok(await Mediator.Send(new GetLevelsQuery()));
    }
}
