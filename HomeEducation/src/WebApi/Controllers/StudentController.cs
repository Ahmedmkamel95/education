using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Commands.StudentCommands;
using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Application.Commands.UserManagementCommands.Dtos;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


public class StudentController : ApiControllerBase
{
    [Route("createStudent")]
    [AllowAnonymous]
    [Produces(typeof(Result<CreateStudentResponseDto>))]
    [HttpPost]
    public async Task<IActionResult> CreateStudentUser([FromBody] AddStudentRequestDto addStudentRequest)
    {
        var result = await Mediator.Send(new CreateStudentCommand(addStudentRequest));
        return !result.Succeeded ? BadRequest(result) : Created("", result);
    }

    [Route("subscribeToCourse")]
    [Authorize(Roles = Role.Student)]
    [Produces(typeof(Result<string>))]
    [HttpPost]
    public async Task<IActionResult> SubscribeToCourse([FromQuery] string courseId)
    {
        var result = await Mediator.Send(new SubscribeToCourseCommand(courseId));
        return !result.Succeeded ? BadRequest(result) : Ok(result);
    }
}
