using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Queries.UserManagementQuesries;
public class GetProfileQuery : IRequest<Result<GetProfileResponseDto>>
{

}

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<GetProfileResponseDto>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<RequestLocalizationOptions> _options;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    public GetProfileQueryHandler(IHomeEducationDbContext context, IMapper mapper, IOptions<RequestLocalizationOptions> options, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _options = options;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<Result<GetProfileResponseDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        if(!string.IsNullOrEmpty(userId))
        {
            string userRole = await _identityService.GetUserRole(userId);

            if(!string.IsNullOrEmpty(userRole))
            {
                var user = userRole switch
                {
                    Role.Student => (User?)_context.Students.FirstOrDefault(u => u.Id == userId),
                    Role.Teacher => _context.Teachers.FirstOrDefault(u => u.Id == userId),
                    Role.Admin => _context.Admins.FirstOrDefault(u => u.Id == userId),
                    _ => null
                };

                return Result<GetProfileResponseDto>.Success(new GetProfileResponseDto() { User = user });
            }
        }
                return Result<GetProfileResponseDto>.Failure(new string[] { "no user found to return profile"});
       
    }
}

