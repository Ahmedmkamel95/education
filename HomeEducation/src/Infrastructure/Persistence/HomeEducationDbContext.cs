using System.Reflection;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Domain.Entities;
using HomeEducation.Domain.Enums;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeEducation.Infrastructure.Persistence;
public class HomeEducationDbContext : DbContext , IHomeEducationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public HomeEducationDbContext(
        DbContextOptions<HomeEducationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Level> Levels => Set<Level>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<TeacherLevel> TeacherLevels => Set<TeacherLevel>();
    public DbSet<TeacherStudent> TeacherStudents => Set<TeacherStudent>();
    public DbSet<StudentCourseEnrollment> StudentCourseEnrollments=> Set<StudentCourseEnrollment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
