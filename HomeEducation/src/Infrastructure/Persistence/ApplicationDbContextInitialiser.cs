using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Entities;
using HomeEducation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _appContext;
    private readonly HomeEducationDbContext _homeEducationContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IIdentityService _identityService;
    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext appContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, HomeEducationDbContext homeEducationContext, IIdentityService identityService)
    {
        _logger = logger;
        _appContext = appContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _homeEducationContext = homeEducationContext;
        _identityService = identityService;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_homeEducationContext.Database.IsSqlServer() && _appContext.Database.IsSqlServer())
            {
                await _appContext.Database.MigrateAsync();
                await _homeEducationContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //Seed Default roles
        await SeedRoles();
        //Seed Default users
        await SeedAdminUser();
    }

    private async Task SeedRoles() {
        var type = typeof(Role);
        var roleProperities = type.GetFields();

        foreach(var role in roleProperities)
        {
            var roleName = role.GetValue(typeof(Role)).ToString();
            var userRole = new IdentityRole(roleName);
            if (_roleManager.Roles.All(r => r.Name != userRole.Name))
            {
                await _roleManager.CreateAsync(userRole);
            }
        }
    }
    private async Task SeedAdminUser() 
    {
       var administratorRole = _roleManager.Roles.FirstOrDefault(x => x.Name == Role.Admin);

        var administrator = new ApplicationUser { UserName = "administrator2", Email = "administrator2@homeEducation" };
        
        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
           await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        if (_homeEducationContext.Admins.All(u => u.Email != administrator.Email))
        {
            User user = new Admin()
            {
                Email = "administrator2@homeEducation",
                Id = administrator.Id,
                FirstName = "administrator2",
                LastName = "administrator",
                PhoneNumber = "01241564864",
                IsActive = true
            };
            await _homeEducationContext.AddAsync(user);
            await _homeEducationContext.SaveChangesAsync();
        }
    }
}
