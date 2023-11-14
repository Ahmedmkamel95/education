using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Dtos.UserManagementDtos;
public class AddUserRequestDto : IUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string? Image { get ; set; }
}
