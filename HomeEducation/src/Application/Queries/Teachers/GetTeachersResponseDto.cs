using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeEducation.Application.Common.Mappings;
using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Queries.Teachers;
public class GetTeachersResponseDto : IMapFrom<Teacher>
{
    public string Id { get; set; }

    public string FirstName { set; get; }

    public string LastName { set; get; }

    public string Email { set; get; }

    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Teacher, GetTeachersResponseDto>();
    }
}
