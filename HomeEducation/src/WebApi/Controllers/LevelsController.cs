using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeEducation.WebApi.Controllers;
public class LevelsController : ApiControllerBase
{

    public LevelsController () {  }

    [HttpGet]
    public async Task<ActionResult<GetLevelsDto>> GetLevels()
    {
        return await Mediator.Send(new GetLevelsQuery());
    }
}
