using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Domain.Entities;
public abstract class User : BaseAuditableEntity
{
    [StringLength(255)]
    public string FirstName { set; get; }

    [StringLength(255)]
    public string LastName { set; get; }

    [EmailAddress]
    [StringLength(255)]
    public string Email { set; get; }

    [Phone]
    [StringLength(255)]
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }
}
