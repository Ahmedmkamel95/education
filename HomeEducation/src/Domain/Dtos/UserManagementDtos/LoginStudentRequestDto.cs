using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Dtos.UserManagementDtos;
public class LoginStudentRequestDto:LoginRequestDto
{
    public string MacAddress { get; set; }
    public string FirebaseToken { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
