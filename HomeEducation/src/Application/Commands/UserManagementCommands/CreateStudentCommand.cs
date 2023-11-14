using HomeEducation.Application.Commands.UserManagementCommands.Dtos;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.UserManagementCommands;
public record CreateStudentCommand : IRequest<Result<CreateStudentResponseDto>>
{
    public CreateStudentCommand(IUserRequest request)
    {
        Request = request;
    }

    public IUserRequest Request { get; set; }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<CreateStudentResponseDto>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ILogger<CreateStudentCommandHandler> _logger;
    public CreateStudentCommandHandler(IHomeEducationDbContext context, IIdentityService identityService, ILogger<CreateStudentCommandHandler> logger)
    {
        _context = context;
        _identityService = identityService;
        _logger = logger;
    }
    public async Task<Result<CreateStudentResponseDto>> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var userRequest = command.Request as AddStudentRequestDto;
        var student = _context.Students.FirstOrDefault(x => x.Email == userRequest.Email);
        if (student != null)
        {
            return Result<CreateStudentResponseDto>.Failure(new string[] { "User is already exists" });
        }

        var createUserResult = await _identityService.CreateUserAsync(userRequest.Email, userRequest.Password, Role.Student);
        if (!createUserResult.Result.Succeeded)
        {
            return Result<CreateStudentResponseDto>.Failure(createUserResult.Result.Errors);
        }
        try
        {
            student = new Student()
            {
                Id = createUserResult.UserId,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
                IsActive = true,
                FirebaseToken = userRequest.FirebaseToken,
                MacAddress = string.Join('|', userRequest.MacAddresses),
                LevelId = userRequest.LevelId
            };

            if (!_context.Levels.Any(level => userRequest.LevelId == level.Id))
            {
                _logger.LogError($"Faild to create user {student.Id}: {student.FirstName} {student.LastName}, Level is wrong ");
                return Result<CreateStudentResponseDto>.Failure(new string[] { $"Faild to create user {student.Id}: {student.FirstName} {student.LastName}, Levels is wrong" });
            }

            await _context.Students.AddAsync(student);
            var result = await _context.SaveChangesAsync(cancellationToken);
            var studentResponse = new CreateStudentResponseDto() { Student = student, Token = createUserResult.Token};
            return Result<CreateStudentResponseDto>.Success(studentResponse);

        }
        catch (Exception ex)
        {
            _logger.LogError($"Faild to create user {student.Id}: {student.FirstName} {student.LastName} ");
            _logger.LogError(ex.Message);

            await _identityService.DeleteUserAsync(student.Id);
            return Result<CreateStudentResponseDto>.Failure(new string[] { $"Faild to create user {student.Id}: {student.FirstName} {student.LastName} " });
        }
    }
}
