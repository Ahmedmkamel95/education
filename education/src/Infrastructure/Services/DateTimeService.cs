using education.Application.Common.Interfaces;

namespace education.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
