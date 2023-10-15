using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HomeEducation.Infrastructure.Identity;
public interface IJwtProvider
{
    string GenerateJwtToken(ApplicationUser user);
}
