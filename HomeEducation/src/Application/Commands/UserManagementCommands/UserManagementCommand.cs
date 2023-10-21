using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.UserManagementCommands;
public record UserManagementCommand : IRequest<Result<string>>
{
    public UserManagementCommand(IUserRequest request, string userType)
    {
        this.Request = request;
        this.UserType = userType;
    }

    public IUserRequest Request {  get; set; }
    public string UserType {  get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<UserManagementCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService, ILogger<CreateUserCommandHandler> logger)
    {
        _context = context;
        _identityService = identityService;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(UserManagementCommand command, CancellationToken cancellationToken)
    {
        IUserRequest userRequest = null;
        
        switch (command.UserType)
        {
            case Role.Admin:
                userRequest = command.Request as AddUserRequestDto;
                break;
            case Role.Teacher:
                userRequest = command.Request as AddTeacherRequestDto;
                break;
            case Role.Student:
                userRequest = command.Request as AddStudentRequestDto;
                break;
        }

        var applicationUser = _context.Users.FirstOrDefault(x => x.Email == userRequest.Email);
        if (applicationUser != null)
        {
            return Result<string>.Failure(new string[] { "User is already exists" });
        }

        var createUserResult = await _identityService.CreateUserAsync(userRequest.Email, userRequest.Password);
        if(!createUserResult.Result.Succeeded)
        {
            return createUserResult.Result;
        }

        applicationUser = new User() {
            Id = createUserResult.UserId,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            PhoneNumber = userRequest.PhoneNumber,
            UserType = command.UserType,
            IsActive = false
        };

        try
        {
            await _context.Users.AddAsync(applicationUser);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("User created successfully");

        }catch (Exception ex)
        {
            _logger.LogError($"Faild to create user {applicationUser.Id}: {applicationUser.FirstName} {applicationUser.LastName} ");
            _logger.LogError(ex.Message);

            await _identityService.DeleteUserAsync(applicationUser.Id);
            return Result<string>.Failure(new string[] { $"Faild to create user {applicationUser.Id}: {applicationUser.FirstName} {applicationUser.LastName} " });
        }
    }
}
