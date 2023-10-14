
using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeEducation.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Level> Levels { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
