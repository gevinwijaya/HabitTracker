using System;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace HabitTracker.Tracker.Database.Postgres
{
  public class PostgresUnitOfWork : UnitOfWork
  {
    private NpgsqlConnection _connection;
    private NpgsqlTransaction _transaction;

    private IHabitRepository _habitRepository;
    private IBadgeRepository _badgeRepository;

    public IHabitRepository HabitRepo
    {
      get
      {
        if (_habitRepository == null)
        {
          _habitRepository = new PostgresHabitRepository(_connection, _transaction);
        }
        return _habitRepository;
      }
    }

    public IBadgeRepository UserRepo
    {
      get
      {
        if (_badgeRepository == null)
        {
          _badgeRepository = new PostgresBadgeRepository(_connection, _transaction);
        }
        return _badgeRepository;
      }
    }

    public PostgresUnitOfWork(string connectionString)
    {
      _connection = new NpgsqlConnection(connectionString);
      _connection.Open();
      _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
      _transaction.Commit();
    }

    public void Rollback()
    {
      _transaction.Rollback();
    }

    private bool disposed = false;
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _connection.Close();
        }

        disposed = true;
      }
    }


  }
}