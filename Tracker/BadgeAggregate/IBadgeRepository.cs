using System;
using System.Collections.Generic;

namespace HabitTracker.Tracker{

    public interface IBadgeRepository{
        List<Badge> GetBadge(Guid user_id);
        void AddBadge(Badge badge);
    }

}