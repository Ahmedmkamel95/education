using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Dtos.UserManagementDtos;
public class AddTeacherRequestDto : AddUserRequestDto
{
    public List<string> CourseIds { get; set; }
    public List<string> LevelIds { get; set; }
}
