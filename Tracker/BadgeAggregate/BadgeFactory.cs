using System;

namespace HabitTracker.Tracker{
    public class BadgeFactory{
        public static Badge Create(Guid user_id, string name, string desc){
            return new Badge(Guid.NewGuid(), name, desc, user_id, DateTime.Now);
        }
    }
}