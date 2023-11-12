using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Application.Queries.Teachers;
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
    [Produces(typeof(Result<string>))]
    [HttpPost]
    public async Task<IActionResult> CreateTeacherUser([FromBody] AddTeacherRequestDto addTeacherRequest/*, IFormFile image*/)
    {
        var result = await Mediator.Send(new CreateTeacherCommand(addTeacherRequest));
        if (!result.Succeeded)
            return BadRequest(result);

        return Created("", result);
    }

    [Route("getAllTeachers")]
    [Produces(typeof(Result<List<GetTeachersResponseDto>>))]
    [HttpGet]
    public async Task<IActionResult> GetAllTeachers()
    {
        return Ok(await Mediator.Send(new GetTeachersQuery()));
    }
}
