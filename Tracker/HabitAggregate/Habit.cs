using System;
using System.Collections.Generic;

namespace HabitTracker.Tracker
{
    public class Habit
    {
        private Guid _id;
        private string _name;
        private DaysOff _days_off;
        private Streak _streak;
        private int _log_count;
        private List<DateTime> _logs;
        private Guid _user_id;
        private DateTime _created_at;

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string[] DaysOff{
            get{
                return _days_off.Days;
            }
        }

        public int CurrentStreak{
            get{
                return _streak.Current;
            }
        }

        public int LongestStreak{
            get{
                return _streak.Longest;
            }
        }

        public int LogCount{
            get{
                return _log_count;
            }
        }

        public List<DateTime> Logs{
            get{
                return _logs;
            }
        }

        public Guid UserId{
            get{
                return _user_id;
            }
        }

        public DateTime CreatedAt{
            get{
                return _created_at;
            }
        }

        public Habit(
            Guid id,
            string name,
            DaysOff days_off,
            Streak streak,
            int log_count,
            List<DateTime> logs,
            Guid user_id,
            DateTime created_at
            )
        {
            this._id = id;
            this._name = name;
            this._days_off = days_off;
            this._streak = streak;
            this._log_count = log_count;
            this._logs = logs;
            this._user_id = user_id;
            this._created_at = created_at;
        }

        public static Habit newHabit(string name, DaysOff days_off, Guid user_id)
        {
            return new Habit(
                Guid.NewGuid(),
                name,
                days_off,
                new Streak(0, 0, 0, DateTime.Now, DateTime.Now),
                0,
                new List<DateTime>(),
                user_id,
                DateTime.Now
                );
        }

        public static Habit newHabit(string name, Guid user_id)
        {
            return new Habit(
                Guid.NewGuid(),
                name,
                new DaysOff(),
                new Streak(0, 0, 0, DateTime.Now, DateTime.Now),
                0,
                new List<DateTime>(),
                user_id,
                DateTime.Now
                );
        }

        public void AddLog()
        {
            _logs.Add(DateTime.Now);
            _log_count = _logs.Count;
            _streak.addStreak(DateTime.Now, _user_id, _id, _days_off.IsDayOff(DateTime.Now));
        }

    }
}