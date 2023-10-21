using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Levels.Quesries;
public record GetLevelsQuery : IRequest<GetLevelsDto>
{
   
}

public class GetLevelsQueryHandler : IRequestHandler<GetLevelsQuery, GetLevelsDto>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<RequestLocalizationOptions> _options;

    public GetLevelsQueryHandler(IHomeEducationDbContext context, IMapper mapper, IOptions<RequestLocalizationOptions> options)
    {
        _context = context;
        _mapper = mapper;
        _options = options;
    }

    public async Task<GetLevelsDto> Handle(GetLevelsQuery request, CancellationToken cancellationToken)
    {
        return new GetLevelsDto
        { 
            Primary = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Primary), opt => opt.Items["culture"] = _options.Value.DefaultRequestCulture.Culture.Name),
            Preparatory = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Preparatory), opt => opt.Items["culture"] = _options.Value.DefaultRequestCulture.Culture.Name),
            Secondary = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Secondary), opt => opt.Items["culture"] = _options.Value.DefaultRequestCulture.Culture.Name),
        };
    }
}
