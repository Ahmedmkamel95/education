using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Authorize(Roles = Role.Admin)]
public class AdminController : ApiControllerBase
{
    [Route("/createTeacher")]
    [HttpPost]
    public async Task<IActionResult> CreateTeacherUser([FromBody] AddTeacherRequestDto addTeacherRequest)
    {
        var result = await Mediator.Send(new UserManagementCommand(addTeacherRequest, Role.Teacher));
        if (!result.Succeeded)
            return BadRequest(result);

        return Created("", result);
    }
}
