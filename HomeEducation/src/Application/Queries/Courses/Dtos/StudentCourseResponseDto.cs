using AutoMapper;
using HomeEducation.Application.Common.Mappings;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Queries.Courses.Dtos;
public class StudentCourseResponseDto : IMapFrom<Course>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public string LevelId { get; set; }
    public string LevelTitle { get; set; }

    public string TeacherName { get; set; }
    public string TeacherId { get; set; }

    public string Image { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, StudentCourseResponseDto>()
            .ForMember(dest=>dest.TeacherName, m=> m.MapFrom(src=> $"{src.Teacher.FirstName} {src.Teacher.LastName}" ))
            .AfterMap((s, d, context) => d.Title = context.TryGetItems(out var Items) ? context.Items["culture"].ToString() == "ar" ? s.TitleAr : s.TitleEn : s.TitleEn)
            .AfterMap((s, d, context) => d.LevelTitle = context.TryGetItems(out var Items) ? context.Items["culture"].ToString() == "ar" ? s.Level.TitleAr : s.Level.TitleEn: s.Level.TitleEn)
            .AfterMap((s, d, context) => d.Description = context.TryGetItems(out var Items) ? context.Items["culture"].ToString() == "ar" ? s.DescriptionAr : s.DescriptionEn : s.DescriptionEn);
    }
}
