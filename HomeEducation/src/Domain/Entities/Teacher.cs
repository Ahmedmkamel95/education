using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class Teacher : User
{
    public ICollection<TeacherLevel> TeacherLevels { get; set; }
    public ICollection<TeacherStudent>? TeacherStudents { get; set; }
    public ICollection<Course> Courses;
}
