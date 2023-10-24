using System.ComponentModel.DataAnnotations.Schema;

namespace HomeEducation.Domain.Entities;
public class TeacherStudent 
{
    [ForeignKey("Teacher")]
    public string TeacherId { get; set; }

    [ForeignKey("Student")]
    public string StudentId{ get; set; }

    public Teacher Teacher { get; set; }
    public Student Student { get; set; }
}
