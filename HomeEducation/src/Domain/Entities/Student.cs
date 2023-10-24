using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class Student : User
{
    public string FirebaseToken { get; set; }
    public string MacAddress { get; set; }

    [ForeignKey("Level")]
    public string? LevelId { get; set; }

    public Level Level { get; set; }
    public ICollection<TeacherStudent>? TeacherStudents { get; set; }
    public ICollection<StudentCourseEnrollment>? StudentCourseEnrollments{ get; set; }

}
