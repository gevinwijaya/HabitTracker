using System;

namespace HabitTracker.Tracker
{
  public interface IObserver<T>
  {
    void Update(T e);
  }

  public interface IObservable<T>
  {
    void Attach(IObserver<T> obs);
    void Broadcast(T e);
  }
}