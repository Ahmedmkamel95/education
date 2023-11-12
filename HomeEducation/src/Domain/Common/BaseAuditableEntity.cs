using System.Text.Json.Serialization;

namespace HomeEducation.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    [JsonIgnore]
    public DateTime Created { get; set; }

    [JsonIgnore]
    public string? CreatedBy { get; set; }

    [JsonIgnore]
    public DateTime? LastModified { get; set; }

    [JsonIgnore]
    public string? LastModifiedBy { get; set; }
}
