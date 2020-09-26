using System;

namespace HabitTracker.Tracker
{
  public interface UnitOfWork : IDisposable
  {
    void Commit();
    void Rollback();
  }
}