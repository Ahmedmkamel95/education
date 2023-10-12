using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;
public class LevelsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetLevelsDto>> GetLevels([FromQuery] GetLevelsQuery query)
    {
        return await Mediator.Send(query);
    }
}
