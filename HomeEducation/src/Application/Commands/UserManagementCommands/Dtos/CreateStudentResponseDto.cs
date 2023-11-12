using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Commands.UserManagementCommands.Dtos;
public class CreateStudentResponseDto
{
    public string Token { get; set; }
    public Student Student { get; set;}
}
