using System;
using System.Collections.Generic;

namespace HabitTracker.Tracker
{
    public class Streak : IObservable<BadgeResult>
    {
        private int _current;
        private int _longest;
        private int _dayOffCounter;
        private DateTime _lastStreak;
        private DateTime _lastLog;

        public int Current
        {
            get
            {
                return _current;
            }
        }

        public int Longest
        {
            get
            {
                return _longest;
            }
        }

        public Streak(int current, int longest, int dayoff, DateTime log, DateTime streak)
        {
            _current = current;
            _longest = longest;
            _dayOffCounter = dayoff;
            _lastLog = log;
            _lastStreak = streak;
        }

        public int GetMax(int a, int b)
        {
            if (a > b) return a;
            return b;
        }
        protected List<IObserver<BadgeResult>> _observers = new List<IObserver<BadgeResult>>();
        public void Attach(IObserver<BadgeResult> obs)
        {
            _observers.Add(obs);
        }

        public void Broadcast(BadgeResult e)
        {
            foreach (var obs in _observers)
            {
                obs.Update(e);
            }
        }

        public Streak addStreak(DateTime date, Guid user_id, Guid habit_id, bool isDayOff)
        {
            Broadcast(new Log(user_id, habit_id));
            // broadcast log
            if ((_current + 1) == 10 && (date.Date - _lastStreak.Date).TotalDays >= 20)
            {
                Broadcast(new EpicComeback(user_id));
            }
            if ((_current + 1) == 4)
            {
                Broadcast(new Dominating(user_id));
            }
            if (isDayOff)
            {
                _dayOffCounter++;
            }
            if (_dayOffCounter == 10)
            {
                Broadcast(new Workaholic(user_id));
            }
            if (_lastLog == null)
            {
                return new Streak(1, GetMax(1, this._longest), this._dayOffCounter, date, DateTime.Now);
            }
            if ((date.Date - _lastLog.Date).TotalDays == 1)
            {
                return new Streak(this._current + 1, GetMax((this._current + 1), this._longest), this._dayOffCounter, date, this._lastStreak);
            }
            if ((date.Date - _lastLog.Date).TotalDays >= 2)
            {
                return new Streak(1, GetMax(1, this._longest), this._dayOffCounter, date, _lastLog);
            }
            else
            {
                return new Streak(_current, _longest, _dayOffCounter, _lastLog, _lastStreak);
            }
        }

        public int getCurrentStreak()
        {
            if ((DateTime.Now.Date - _lastLog.Date).TotalDays > 1)
            {
                _lastStreak = _lastLog;
                _current = 0;
            }
            return _current;
        }

        public int getLongestStreak()
        {
            return _longest;
        }

    }
}