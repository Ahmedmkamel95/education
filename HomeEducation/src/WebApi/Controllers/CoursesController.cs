using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Commands.CourseCommands.Dtos;
using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;

public class CoursesController : ApiControllerBase
{

    public CoursesController() { }

    [Authorize(Roles = $"{Role.Admin},{Role.Teacher}")]
    [Route("createCourse")]
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody]CreateCourseRequestDto request)
    {
        return Ok(await Mediator.Send(new CreateCourseCommand(request)));
    }
}
