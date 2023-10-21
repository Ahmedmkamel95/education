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
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
