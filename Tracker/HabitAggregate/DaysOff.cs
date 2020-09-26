using System;
using System.Collections.Generic;

namespace HabitTracker.Tracker
{
    public class DaysOff
    {
        private string[] _days;

        public string[] Days
        {
            get
            {
                return _days;
            }
        }

        public DaysOff()
        {
            _days = new string[] { };
        }
        
        public DaysOff(string[] days)
        {
            List<string> d = new List<string>();
            for (int i = 0; i < days.Length; i++)
            {
                if(d.Contains(days[i]) == false){
                    d.Add(days[i]);
                }
            }
            if (days.Length != d.Count)
            {
                throw new Exception("days off cannot be the same");
            }
            for (int i = 0; i < days.Length; i++)
            {
                if (
                    days[i] != "Mon" ||
                    days[i] != "Tue" ||
                    days[i] != "Wed" ||
                    days[i] != "Thu" ||
                    days[i] != "Fri" ||
                    days[i] != "Sat" ||
                    days[i] != "Sun"
                )
                {
                    throw new Exception("days off must be appropriate");
                }
            }
            if (days.Length == 7)
            {
                throw new Exception("days off cannot be everyday");
            }

            this._days = days;

        }

        public bool IsDayOff(DateTime date)
        {

            for (int i = 0; i < _days.Length; i++)
            {
                if (_days[i] == date.ToString("ddd"))
                {
                    return true;
                }
            }
            return false;
        }



    }

}