using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.AdminCommands;
public record CreateAdminCommand : IRequest<Result<string>>
{
    public CreateAdminCommand(IUserRequest request)
    {
        Request = request;
    }

    public IUserRequest Request { get; set; }
}

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Result<string>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ILogger<CreateAdminCommandHandler> _logger;
    public CreateAdminCommandHandler(IHomeEducationDbContext context, IIdentityService identityService, ILogger<CreateAdminCommandHandler> logger)
    {
        _context = context;
        _identityService = identityService;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(CreateAdminCommand command, CancellationToken cancellationToken)
    {
        var userRequest = command.Request as AddUserRequestDto;
        var admin = _context.Admins.FirstOrDefault(x => x.Email == userRequest.Email);
        if (admin != null)
        {
            return Result<string>.Failure(new string[] { "Admin is already exists" });
        }

        var createUserResult = await _identityService.CreateUserAsync(userRequest.Email, userRequest.Password, userRequest.PhoneNumber, Role.Admin);
        if (!createUserResult.Result.Succeeded)
        {
            return createUserResult.Result;
        }

        admin = new Admin()
        {
            Id = createUserResult.UserId,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            PhoneNumber = userRequest.PhoneNumber,
            IsActive = false
        };

        try
        {
            await _context.Admins.AddAsync(admin);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("Admin created successfully");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Faild to create admin {admin.Id}: {admin.FirstName} {admin.LastName} ");
            _logger.LogError(ex.Message);

            await _identityService.DeleteUserAsync(admin.Id);
            return Result<string>.Failure(new string[] { $"Faild to create Admin {admin.Id}: {admin.FirstName} {admin.LastName} " });
        }
    }
}
