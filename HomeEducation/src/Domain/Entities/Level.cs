using HomeEducation.Domain.Enums;

namespace HomeEducation.Domain.Entities;

public class Level : BaseAuditableEntity
{
    public string TitleEn { get; set; }
    public string TitleAr { get; set; }
    public StudyPhase Phase { get; set; }

}
