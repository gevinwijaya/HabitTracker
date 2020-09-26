using System;

namespace HabitTracker.Tracker
{
    public abstract class BadgeResult
    {
        public Guid User_id { get; private set; }
        public BadgeResult(Guid user_id)
        {
            this.User_id = user_id;
        }
    }

    public class Dominating : BadgeResult
    {
        public Dominating(Guid user_id) : base(user_id) { }
    }

    public class Workaholic : BadgeResult
    {
        public Workaholic(Guid user_id) : base(user_id) { }
    }

    public class EpicComeback : BadgeResult
    {
        public EpicComeback(Guid user_id) : base(user_id) { }
    }

    public class Log : BadgeResult{
        public Guid Habit_id;
        public Log(Guid user_id, Guid habit_id) : base(user_id) { 
            this.Habit_id = habit_id;
        }
    }
}