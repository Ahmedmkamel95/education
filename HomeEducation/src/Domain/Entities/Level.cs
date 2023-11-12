using System.Text.Json.Serialization;
using HomeEducation.Domain.Enums;

namespace HomeEducation.Domain.Entities;

public class Level : BaseAuditableEntity
{
    public string TitleEn { get; set; }
    public string TitleAr { get; set; }
    public StudyPhase Phase { get; set; }

    [JsonIgnore]
    public ICollection<TeacherLevel> TeacherLevels { get; set; }
    [JsonIgnore]
    public ICollection<Course> Courses { get; set; }
    [JsonIgnore]
    public ICollection<Student> Students { get; set; }

}
