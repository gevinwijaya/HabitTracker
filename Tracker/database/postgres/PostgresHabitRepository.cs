using System;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;


namespace HabitTracker.Tracker.Database.Postgres
{
    public class PostgresHabitRepository : IHabitRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public PostgresHabitRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<DateTime> GetLog(Guid habit_id)
        {
            List<DateTime> log = new List<DateTime>();
            string query = "select date from Habit_Snapshot where habit_id = @habit_id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    log.Add(date);
                }
                reader.Close();
                return log;
            }
        }

        public int GetLogCount(Guid habit_id){
            int log_count = 0;
            string query = "select count(date) from Habit_Snapshot where habit_id = @habit_id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    log_count = reader.GetInt32(0);
                }
                reader.Close();
            }
            return log_count;
        }

        public List<Habit> GetHabit(Guid user_id)
        {
            List<Habit> habit = new List<Habit>();
            string query = "select id, name, days_off, current_streak, longest_streak, user_id, created_at from \"Habit\" where user_id = @user_id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("user_id", user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid id = reader.GetGuid(0);
                    string name = reader.GetString(1);
                    string[] days = reader.GetFieldValue<string[]>(2);
                    int current_streak = reader.GetInt32(3);
                    int longest_streak = reader.GetInt32(4);
                    List<DateTime> logs = GetLog(user_id);
                    int log_count = GetLogCount(user_id);
                    Guid userid = reader.GetGuid(5);
                    DateTime created_at = reader.GetDateTime(6);
                    habit.Add(new Habit(id, name, new DaysOff(days), new Streak(current_streak, longest_streak, 0, DateTime.Now, DateTime.Now), log_count, logs, userid, created_at));
                }
                reader.Close();
            }
            return habit;
        }
        public Habit GetHabitById(Guid user_id, Guid habit_id)
        {
            Habit habit;
            string query = "select id, name, days_off, current_streak, longest_streak, user_id, created_at from \"Habit\" where id = @id and user_id = @user_id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()){
                    Guid id = reader.GetGuid(0);
                    string name = reader.GetString(1);
                    string[] days = reader.GetFieldValue<string[]>(2);
                    int current_streak = reader.GetInt32(3);
                    int longest_streak = reader.GetInt32(4);
                    List<DateTime> logs = GetLog(habit_id);
                    int log_count = GetLogCount(habit_id);
                    Guid userid = reader.GetGuid(5);
                    DateTime created_at = reader.GetDateTime(6);
                    habit = (new Habit(id, name, new DaysOff(days), new Streak(current_streak, longest_streak, 0, DateTime.Now, DateTime.Now), log_count, logs, userid, created_at));
                }
                else{
                    return null;
                }
                reader.Close();
            }
            return habit;
        }
        public void Create(Habit habit)
        {
            string query = "insert into \"Habit\" (id, name, days_off, current_streak, longest_streak, user_id, created_at) values (@id, @name, @days_off, @current_streak, @longest_streak, @user_id, @created_at)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", habit.ID);
                cmd.Parameters.AddWithValue("name", habit.Name);
                cmd.Parameters.AddWithValue("days_off", habit.DaysOff);
                cmd.Parameters.AddWithValue("current_streak", habit.CurrentStreak);
                cmd.Parameters.AddWithValue("longest_streak", habit.LongestStreak);
                cmd.Parameters.AddWithValue("user_id", habit.UserId);
                cmd.Parameters.AddWithValue("created_at", habit.CreatedAt);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(Habit habit, string name, DaysOff daysOff)
        {
            string query = "update habit set name = @name, days_off = @days_off where id = @id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("days_off", daysOff);
                cmd.Parameters.AddWithValue("id", habit.ID);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Guid habit_id)
        {
            string query = "delete from habit where id = @id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", habit_id);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddLog(Guid user_id, Guid habit_id, DateTime date)
        {
            string query = "insert into Habit_Snapshot (date, habit_id) values (@date, @habit_id)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}