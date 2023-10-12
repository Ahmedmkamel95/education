using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Duende.IdentityServer.EntityFramework.Options;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Domain.Entities;
using HomeEducation.Domain.Enums;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeEducation.Infrastructure.Persistence;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Level> Levels => Set<Level>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<Level>().HasData(
            new Level { Id = "1Primary", Title = "First", Phase = StudyPhase.Primary },
            new Level { Id = "2Primary", Title = "Second", Phase = StudyPhase.Primary },
            new Level { Id = "3Primary", Title = "Third", Phase = StudyPhase.Primary },
            new Level { Id = "4Primary", Title = "Fourth", Phase = StudyPhase.Primary },
            new Level { Id = "5Primary", Title = "Fifth", Phase = StudyPhase.Primary },
            new Level { Id = "6Primary", Title = "Sixth", Phase = StudyPhase.Primary },

            new Level { Id = "1Prepare", Title = "First", Phase = StudyPhase.Preparatory },
            new Level { Id = "2Prepare", Title = "Second", Phase = StudyPhase.Preparatory },
            new Level { Id = "3Prepare", Title = "Third", Phase = StudyPhase.Preparatory },

            new Level { Id = "1Secondary", Title = "First", Phase = StudyPhase.Secondary },
            new Level { Id = "2Secondary", Title = "Second", Phase = StudyPhase.Secondary },
            new Level { Id = "3Secondary", Title = "Third", Phase = StudyPhase.Secondary }

            );
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
