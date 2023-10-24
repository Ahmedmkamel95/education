using HomeEducation.Domain.Enums;

namespace HomeEducation.Domain.Entities;

public class Level : BaseAuditableEntity
{
    public string TitleEn { get; set; }
    public string TitleAr { get; set; }
    public StudyPhase Phase { get; set; }

    public ICollection<TeacherLevel> TeacherLevels { get; set; }
    public ICollection<Course> Courses { get; set; }
    public ICollection<Student> Students { get; set; }

}
