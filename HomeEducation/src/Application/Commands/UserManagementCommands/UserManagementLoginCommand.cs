using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using MediatR;

namespace HomeEducation.Application.Commands.UserManagementCommands;
public record UserManagementLoginCommand : IRequest<Result<string>>
{
    public UserManagementLoginCommand(LoginRequestDto request)
    {
        this.Request = request;
    }

    public LoginRequestDto Request { get; set; }
}


public class LoginUserCommandHandler : IRequestHandler<UserManagementLoginCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }
    public async Task<Result<string>> Handle(UserManagementLoginCommand command, CancellationToken cancellationToken)
    {
        var applicationUser = _context.Users.FirstOrDefault(x => x.Email == command.Request.Email);
        if(applicationUser == null)
        {
            return Result<string>.Failure(new string[] { "Invalid Credentials: Wrong Email" });
        }
        var token = await _identityService.AuthenticateUserAsync(applicationUser.Email, command.Request.Password);

        return token;
    }
}