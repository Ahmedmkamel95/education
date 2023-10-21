using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Enums;

namespace HomeEducation.Domain.Entities;
public class User : BaseAuditableEntity
{
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(255)]
    public string FirstName { set; get; }

    [StringLength(255)]
    public string LastName { set; get; }

    [Phone]
    [StringLength(255)]
    public string PhoneNumber { get; set; }

    public string UserType { get; set; }
    
    public bool IsActive { get; set; }

    public string? ImageUrl {  get; set; } 
}
