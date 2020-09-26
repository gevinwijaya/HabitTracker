using System;

using HabitTracker.Tracker.BadgeGiver;
using HabitTracker.Tracker;

namespace HabitTracker.Tracker
{
    public abstract class BadgeHandler : IObserver<BadgeResult>
    {
        protected IBadgeRepository badgeRepository;
        protected BadgeFactory badgeFactory;
        protected IHabitRepository habitRepository;
        protected IBadgeGiver _giver;

        public BadgeHandler(IBadgeRepository repo, IBadgeGiver giver, BadgeFactory factory)
        {
            badgeRepository = repo;
            _giver = giver;
            badgeFactory = factory;
        }

        public abstract void Update(BadgeResult e);
    }

    public abstract class DayLogHandler : IObserver<BadgeResult>
    {
        protected IHabitRepository habitRepository;

        public DayLogHandler(IHabitRepository repo)
        {
            habitRepository = repo;
        }

        public abstract void Update(BadgeResult e);
    }

    public class DominatingHandler : BadgeHandler
    {
    public DominatingHandler(IBadgeRepository repo, IBadgeGiver giver, BadgeFactory factory) : base(repo,giver,factory) { }

        public override void Update(BadgeResult e){
            if(badgeRepository == null) return;

            Dominating ev = e as Dominating;
            if(ev == null) return;

            Badge b = BadgeFactory.Create(ev.User_id, _giver.Give(), _giver.Desc());
            badgeRepository.AddBadge(b);
        }
    }

    public class WorkaholicHandler : BadgeHandler
    {
    public WorkaholicHandler(IBadgeRepository repo, IBadgeGiver giver, BadgeFactory factory) : base(repo,giver,factory) { }

        public override void Update(BadgeResult e){
            if(badgeRepository == null) return;

            Workaholic ev = e as Workaholic;
            if(ev == null) return;

            Badge b = BadgeFactory.Create(ev.User_id, _giver.Give(), _giver.Desc());
            badgeRepository.AddBadge(b);
        }
    }

    public class EpicComebackHandler : BadgeHandler
    {
    public EpicComebackHandler(IBadgeRepository repo, IBadgeGiver giver, BadgeFactory factory) : base(repo,giver,factory) { }

        public override void Update(BadgeResult e){
            if(badgeRepository == null) return;

            EpicComeback ev = e as EpicComeback;
            if(ev == null) return;

            Badge b = BadgeFactory.Create(ev.User_id, _giver.Give(), _giver.Desc());
            badgeRepository.AddBadge(b);
        }
    }

    public class LogHandler : DayLogHandler
    {
    public LogHandler(IHabitRepository repo) : base(repo) { }

        public override void Update(BadgeResult e){
            if(habitRepository == null) return;

            Log ev = e as Log;
            if(ev == null) return;

            habitRepository.AddLog(ev.User_id, ev.Habit_id, DateTime.Now);
        }
    }

    
}

