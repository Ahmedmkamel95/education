using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Authorize(Roles = Role.Admin)]
public class AdminController : ApiControllerBase
{
    [Route("createTeacher")]
    [HttpPost]
    public async Task<IActionResult> CreateTeacherUser([FromBody] AddTeacherRequestDto addTeacherRequest/*, IFormFile image*/)
    {
        var result = await Mediator.Send(new CreateTeacherCommand(addTeacherRequest));
        if (!result.Succeeded)
            return BadRequest(result);

        return Created("", result);
    }

    [Route("createStudent")]
    [HttpPost]
    public async Task<IActionResult> CreateStudentUser([FromBody] AddStudentRequestDto addStudentRequest)
    {
        var result = await Mediator.Send(new CreateStudentCommand(addStudentRequest));
        if (!result.Succeeded)
            return BadRequest(result);

        return Created("", result);
    }

    [Route("getAllTeachers")]
    [HttpGet]
    public async Task<IActionResult> GetAllTeachers()
    {
        return Ok(await Mediator.Send(new GetTeachersQuery()));
    }
}
