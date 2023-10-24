using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEducation.Infrastructure.Data.Configuration;
public class StudentCourseEnrollmentConfiguration : IEntityTypeConfiguration<StudentCourseEnrollment>
{
    public void Configure(EntityTypeBuilder<StudentCourseEnrollment> builder)
    {
        builder.HasKey(s => new { s.StudentId, s.CourseId });
        builder.HasOne(s => s.Student).WithMany(s => s.StudentCourseEnrollments).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(s => s.Course).WithMany(s => s.StudentCourseEnrollments).OnDelete(DeleteBehavior.NoAction);
    }
}
