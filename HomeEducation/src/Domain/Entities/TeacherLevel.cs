using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class TeacherLevel
{
    [ForeignKey("Teacher")]
    public string TeacherId { get; set; }

    [ForeignKey("Level")]
    public string LevelId { get; set; }

    public Teacher Teacher { get; set; }
    public Level Level{ get; set; }
}
