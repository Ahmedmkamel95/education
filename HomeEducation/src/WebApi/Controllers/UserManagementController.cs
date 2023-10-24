using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class UserManagementController : ApiControllerBase
{
      public UserManagementController()
    {
       
    }

    [Route("/api/identity/login")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequest)
    {
        var result = await Mediator.Send(new UserManagementLoginCommand(loginRequest));
        if(result.Succeeded)
            return Ok(result);

        return Unauthorized(result);
    }
}
