
using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeEducation.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
