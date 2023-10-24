using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEducation.Infrastructure.Data.Configuration;
public class TeacherStudentConfiguration : IEntityTypeConfiguration<TeacherStudent>
{
    public void Configure(EntityTypeBuilder<TeacherStudent> builder)
    {
        builder.HasKey(s => new { s.TeacherId, s.StudentId });
        builder.HasOne(s => s.Teacher).WithMany(s => s.TeacherStudents).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(s => s.Student).WithMany(s => s.TeacherStudents).OnDelete(DeleteBehavior.Cascade);

    }
}
