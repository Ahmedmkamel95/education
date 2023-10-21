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
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
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
        var roleProperities = typeof(Role).GetProperties();

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

        var administrator = new ApplicationUser { UserName = "administrator", Email = "administrator@homeEducation" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        if (_context.Users.All(u => u.Email != administrator.Email))
        {
            User user = new User()
            {
                Email = "administrator@homeEducation",
                Id = Guid.NewGuid().ToString(),
                FirstName = "administrator",
                LastName = "administrator",
                UserType = Role.Admin,
                PhoneNumber = "01241564864"
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
