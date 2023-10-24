using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEducation.Infrastructure.Data.Configuration;
public class TeacherLevelConfiguration : IEntityTypeConfiguration<TeacherLevel>
{
    public void Configure(EntityTypeBuilder<TeacherLevel> builder)
    {
        builder.HasKey(s => new { s.TeacherId, s.LevelId });
        builder.HasOne(s => s.Teacher).WithMany(s => s.TeacherLevels).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(s => s.Level).WithMany(s => s.TeacherLevels).OnDelete(DeleteBehavior.Cascade);
    }
}
