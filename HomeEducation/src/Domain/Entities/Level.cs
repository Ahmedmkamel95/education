using HomeEducation.Domain.Enums;

namespace HomeEducation.Domain.Entities;

public class Level : BaseAuditableEntity
{
    public string Title { get; set; }
    public StudyPhase Phase { get; set; }

}
