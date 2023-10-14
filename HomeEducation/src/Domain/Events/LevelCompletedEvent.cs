using HomeEducation.Domain.Entities;

namespace HomeEducation.Domain.Events;

public class LevelCompletedEvent : BaseEvent
{
    public LevelCompletedEvent(Level level)
    {
        Level = level ?? throw new ArgumentNullException(nameof(level));
    }

    public Level Level { get; }
}
