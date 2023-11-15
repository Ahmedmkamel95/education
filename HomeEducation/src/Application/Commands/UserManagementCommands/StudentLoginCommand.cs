using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using MediatR;

namespace HomeEducation.Application.Commands.StudentCommands;
public record StudentLoginCommand : IRequest<Result<string>>
{
    public StudentLoginCommand(LoginStudentRequestDto request)
    {
        this.Request = request;
    }

    public LoginStudentRequestDto Request { get; set; }
}


public class StudentLoginCommandHandler : IRequestHandler<StudentLoginCommand, Result<string>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IIdentityService _identityService;

    public StudentLoginCommandHandler(IHomeEducationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }
    public async Task<Result<string>> Handle(StudentLoginCommand command, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(command.Request.PhoneNumber) && string.IsNullOrEmpty(command.Request.Email))
        {
            return Result<string>.Failure(new string[] { "Login with phone number or email" });
        }
        var result = await _identityService.AuthenticateStudentAsync(command.Request.Email, command.Request.PhoneNumber, command.Request.Password);
        if(result.Succeeded)
        {
            var student = _context.Students.FirstOrDefault(x => x.Email == command.Request.Email || x.PhoneNumber == command.Request.PhoneNumber);
            var macAddresses = student.MacAddress.Split('|');
            if (!student.MacAddress.Contains(command.Request.MacAddress) && macAddresses.Length == 2)
            {
                return Result<string>.Failure(new string[]{"you are prevented from login "});
            }else if(!student.MacAddress.Contains(command.Request.MacAddress))
                student.MacAddress = student.MacAddress + "|" + command.Request.MacAddress;
            student.FirebaseToken = command.Request.FirebaseToken;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return result;
    }
}