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
            new Level { Id = "1Primary", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Primary },
            new Level { Id = "2Primary", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Primary },
            new Level { Id = "3Primary", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Primary },
            new Level { Id = "4Primary", TitleEn = "Fourth", TitleAr = "الرابع", Phase = StudyPhase.Primary },
            new Level { Id = "5Primary", TitleEn = "Fifth", TitleAr = "الخامس", Phase = StudyPhase.Primary },
            new Level { Id = "6Primary", TitleEn = "Sixth", TitleAr = "السادس", Phase = StudyPhase.Primary },

            new Level { Id = "1Prepare", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Preparatory },
            new Level { Id = "2Prepare", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Preparatory },
            new Level { Id = "3Prepare", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Preparatory },

            new Level { Id = "1Secondary", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Secondary },
            new Level { Id = "2Secondary", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Secondary },
            new Level { Id = "3Secondary", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Secondary }

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
