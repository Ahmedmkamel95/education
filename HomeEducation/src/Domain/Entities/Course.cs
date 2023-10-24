﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public string TitleEn { get; set; }
    public string TitleAr { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionAr { get; set; }

    [ForeignKey("Level")]
    public string? LevelId { get; set; }

    [ForeignKey("Teacher")]
    public string TeacherId { get; set; }

    public string Image { get; set; }

    public Teacher Teacher { get; set; }
    public Level Level { get; set; }
    public ICollection<StudentCourseEnrollment>? StudentCourseEnrollments { get; set; }

}
