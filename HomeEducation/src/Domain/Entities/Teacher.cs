using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class Teacher : User
{
    [JsonIgnore]
    public ICollection<TeacherLevel> TeacherLevels { get; set; }
    [JsonIgnore]
    public ICollection<TeacherStudent>? TeacherStudents { get; set; }
    [JsonIgnore]
    public ICollection<Course> Courses;
}
