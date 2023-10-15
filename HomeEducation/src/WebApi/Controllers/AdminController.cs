using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class AdminController : ApiControllerBase
{
    [Route("/createuser")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddTeacherRequestDto addTeacherRequest)
    {
        var result = await Mediator.Send(new UserManagementCommand(addTeacherRequest, HomeEducation.Domain.Enums.ApplicationUserTypes.Teacher));
        if (!result.Succeeded)
            return BadRequest(result);

        return Created("", result);
    }
}
