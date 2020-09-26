using System;

namespace HabitTracker.Tracker.BadgeGiver{
    public class Dominating : IBadgeGiver{
        public string Give(){
            return "Dominating";
        }

        public string Desc(){
            return "4+ streak";
        }
    }

    public class Workaholic : IBadgeGiver{
        public string Give(){
            return "Workaholic";
        }

        public string Desc(){
            return "Doing some works on days-off";
        }
    }

    public class EpicComeback : IBadgeGiver{
        public string Give(){
            return "Epic Comeback";
        }

        public string Desc(){
            return "10 streak after 10 days without logging";
        }
    }
}