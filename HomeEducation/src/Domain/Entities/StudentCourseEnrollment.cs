using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class StudentCourseEnrollment
{
    [ForeignKey("Student")]
    public string StudentId { get; set; }

    [ForeignKey("Course")]
    public string CourseId { get; set; }


    public Course Course { get; set; }
    public Student Student { get; set; }
}
