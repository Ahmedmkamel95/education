using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.AdminCommands;
public record CreateTeacherCommand : IRequest<Result<string>>
{
    public CreateTeacherCommand(IUserRequest request)
    {
        Request = request;
    }

    public IUserRequest Request { get; set; }
}

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Result<string>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ILogger<CreateTeacherCommandHandler> _logger;
    public CreateTeacherCommandHandler(IHomeEducationDbContext context, IIdentityService identityService, ILogger<CreateTeacherCommandHandler> logger)
    {
        _context = context;
        _identityService = identityService;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        var userRequest = command.Request as AddTeacherRequestDto;
        var teacher = _context.Teachers.FirstOrDefault(x => x.Email == userRequest.Email);
        if (teacher != null)
        {
            return Result<string>.Failure(new string[] { "User is already exists" });
        }
        try
        {
            teacher = new Teacher()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
                IsActive = true,
            };
            var levels = _context.Levels.Where(level => userRequest.LevelIds.Contains(level.Id));
            if (!levels.Any())
            {
                _logger.LogError($"Faild to create user {teacher.Id}: {teacher.FirstName} {teacher.LastName}, Levels are wrong ");
                return Result<string>.Failure(new string[] { $"Faild to create user {teacher.Id}: {teacher.FirstName} {teacher.LastName}, Levels are wrong" });
            }

            var createUserResult = await _identityService.CreateUserAsync(userRequest.Email, userRequest.Password, Role.Teacher);
            if (!createUserResult.Result.Succeeded)
            {
                return createUserResult.Result;
            }
            teacher.Id = createUserResult.UserId;
            
            foreach (var item in levels)
            {
                await _context.TeacherLevels.AddAsync(new TeacherLevel { TeacherId = teacher.Id, LevelId = item.Id });
            }
            await _context.Teachers.AddAsync(teacher);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("User created successfully");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Faild to create user {teacher.Id}: {teacher.FirstName} {teacher.LastName} ");
            _logger.LogError(ex.Message);

            await _identityService.DeleteUserAsync(teacher.Id);
            return Result<string>.Failure(new string[] { $"Faild to create user {teacher.Id}: {teacher.FirstName} {teacher.LastName} " });
        }
    }
}
