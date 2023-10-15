using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class LevelsController : ApiControllerBase
{

    public LevelsController () {  }

    [HttpGet]
    public async Task<ActionResult<GetLevelsDto>> GetLevels()
    {
        return await Mediator.Send(new GetLevelsQuery());
    }
}
