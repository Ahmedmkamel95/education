using AutoMapper;
using HomeEducation.Application.Common.Mappings;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Queries.GetLevels;
public class GetLevelsDto
{
    public List<PhaseGradeDto> Primary {  get; set; }
    public List<PhaseGradeDto> Preparatory {  get; set; }
    public List<PhaseGradeDto> Secondary {  get; set; }

}

public class PhaseGradeDto:IMapFrom<Level>
{
    public string Id { get; set; }
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Level, PhaseGradeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .AfterMap((s,d,context) => d.Title = context.TryGetItems(out var Items) ? context.Items["culture"].ToString() == "ar" ? s.TitleAr : s.TitleEn : s.TitleEn);
    }
}