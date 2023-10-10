using HomeEducation.Application.Common.Interfaces;

namespace HomeEducation.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
