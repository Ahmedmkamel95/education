using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEducation.Application.Commands.CourseCommands.Dtos;
public class CreateCourseRequestDto
{
    public string TitleAr {  get; set; }
    public string TitleEn {  get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string LevelId { get; set; }
    public string TeacherId { get; set; }
    public string Image {  get; set; }
}
