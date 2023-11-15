using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeEducation.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IJwtProvider _jwtProvider;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IJwtProvider jwtProvider,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _jwtProvider = jwtProvider;
        _roleManager = roleManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }
    public async Task<string?> GetUserRole(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
        var userRole = await _userManager.GetRolesAsync(user);

        return userRole?.FirstOrDefault();
    }
    public async Task<(Result<string> Result, string UserId, string Token)> CreateUserAsync(string userName, string password, string phoneNumber, string role)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
            PhoneNumber = phoneNumber
        };

        var result = await _userManager.CreateAsync(user, password);
        var sysRole = _roleManager.Roles.FirstOrDefault(x => x.Name == role);
        if (!string.IsNullOrWhiteSpace(sysRole.Name))
        {
            await _userManager.AddToRolesAsync(user, new[] { sysRole.Name });
        }

        //create token for student
        if(role == Role.Student)
        {
            var token = _jwtProvider.GenerateJwtToken(user, Role.Student);
            return (result.ToApplicationResult(), user.Id, token);
        }
        return (result.ToApplicationResult(), user.Id, "");
    }
    public async Task<Result<string>> AuthenticateUserAsync(string email, string password)
    {
        bool isAuthenticated = false;
        var user = await _userManager.FindByEmailAsync(email);
        if(user != null)
            isAuthenticated = await _userManager.CheckPasswordAsync(user, password);
        if(!isAuthenticated)
        {
            return Result<string>.Failure(new string[] { "Authentication Faild, Wrong Credentials "});
        }
        var userRole = await _userManager.GetRolesAsync(user);
        var token = _jwtProvider.GenerateJwtToken(user, userRole.FirstOrDefault());
        return Result<string>.Success(token);
    }

    public async Task<Result<string>> AuthenticateStudentAsync(string email, string phoneNumber, string password)
    {
        bool isAuthenticated = false;

        ApplicationUser user = null;
        if (email != null)
            user = await _userManager.FindByEmailAsync(email);
        else if(phoneNumber != null)
            user = await _userManager.Users.SingleOrDefaultAsync( x => x.PhoneNumber == phoneNumber);
        if (user != null)
            isAuthenticated = await _userManager.CheckPasswordAsync(user, password);
        if (!isAuthenticated)
        {
            return Result<string>.Failure(new string[] { "Authentication Faild, Wrong Credentials " });
        }
        var userRole = await _userManager.GetRolesAsync(user);
        var token = _jwtProvider.GenerateJwtToken(user, userRole.FirstOrDefault());
        return Result<string>.Success(token);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = new { Succeeded  = true};//await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result<string>> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result<string>.Success("");
    }

    public async Task<Result<string>> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

   
}
