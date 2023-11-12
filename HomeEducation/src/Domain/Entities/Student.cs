using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeEducation.Domain.Entities;
public class Student : User
{
    public string FirebaseToken { get; set; }
    public string MacAddress { get; set; }

    [ForeignKey("Level")]
    public string? LevelId { get; set; }

    [JsonIgnore]
    public Level Level { get; set; }

    [JsonIgnore]
    public ICollection<TeacherStudent>? TeacherStudents { get; set; }
    [JsonIgnore]
    public ICollection<StudentCourseEnrollment>? StudentCourseEnrollments{ get; set; }

}
