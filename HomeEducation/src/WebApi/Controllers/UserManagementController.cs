using HomeEducation.Application.Commands.UserManagementCommands;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;
[Route("/api/user")]
public class UserManagementController : ApiControllerBase
{
    
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    public UserManagementController(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
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
    [Produces(typeof(Result<UserProfile<User>>))]
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        if (!string.IsNullOrEmpty(userId))
        {
            string userRole = await _identityService.GetUserRole(userId);

            if (!string.IsNullOrEmpty(userRole))
            {
                switch (userRole)
                {
                    case Role.Student:
                        return Ok(await Mediator.Send(new GetStudentProfileQuery(userId)));
                        break;
                    case Role.Teacher:
                        return Ok(await Mediator.Send(new GetTeacherProfileQuery(userId)));
                        break;
                    case Role.Admin:
                        return Ok(await Mediator.Send(new GetAdminProfileQuery(userId)));
                        break;
                }
            }
        }
        return BadRequest("User not found");
    }
}
