using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeEducation.Application.Common.Interfaces;
public interface IHomeEducationDbContext
{
    DbSet<Level> Levels { get; }
    DbSet<Admin> Admins { get; }
    DbSet<Teacher> Teachers { get; }
    DbSet<Student> Students { get; }
    DbSet<Course> Courses { get; }
    DbSet<TeacherLevel> TeacherLevels { get; }
    DbSet<TeacherStudent> TeacherStudents{ get; }
    DbSet<StudentCourseEnrollment> StudentCourseEnrollments{ get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
