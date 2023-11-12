using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("/api/user")]
public class UserManagementController : ApiControllerBase
{
      public UserManagementController()
    {
       
    }

    [Route("login")]
    [AllowAnonymous]
    [Produces(typeof(Result<string>))]
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequest)
    {
        var result = await Mediator.Send(new UserManagementLoginCommand(loginRequest));
        if(result.Succeeded)
            return Ok(result);

        return Unauthorized(result);
    }

    [Route("profile")]
    [Authorize(Roles = $"{Role.Student}, {Role.Teacher}, {Role.Admin}")]
    [Produces(typeof(Result<GetProfileResponseDto>))]
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var result = await Mediator.Send(new GetProfileQuery());
        return Ok(result);
    }
}
