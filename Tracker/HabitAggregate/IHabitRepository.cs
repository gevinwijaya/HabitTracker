using System;
using System.Collections.Generic;

namespace HabitTracker.Tracker{
    public interface IHabitRepository{
        List<Habit> GetHabit(Guid user_id);
        Habit GetHabitById(Guid user_id, Guid habit_id);
        void Create(Habit habit);
        void Update(Habit habit, string name, DaysOff daysOff);
        void Delete(Guid habit_id);
        void AddLog(Guid user_id, Guid habit_id, DateTime date);
    }
}