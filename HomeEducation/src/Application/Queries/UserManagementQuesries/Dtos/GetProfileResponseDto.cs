using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
public class GetProfileResponseDto
{
    public User User { get; set; }
}
