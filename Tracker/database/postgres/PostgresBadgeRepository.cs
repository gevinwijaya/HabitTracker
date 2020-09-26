using System;
using System.Collections.Generic; 

using Npgsql;
using NpgsqlTypes;

namespace HabitTracker.Tracker.Database.Postgres
{

    public class PostgresBadgeRepository : IBadgeRepository
    {   
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public PostgresBadgeRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public List<Badge> GetBadge(Guid user_id)
        {
            List<Badge> badge = new List<Badge>();
            string query = "select id, name, description, user_id, created_at from Badge where user_id = @user_id";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("user_id", user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid id = reader.GetGuid(0);
                    string name = reader.GetString(1);
                    string desc = reader.GetString(2);
                    Guid userid = reader.GetGuid(3);
                    DateTime created_at = reader.GetDateTime(4);
                    badge.Add(new Badge(id, name, desc, userid, created_at));
                }
                reader.Close();
            }
            return badge;
        }
        public void AddBadge(Badge badge)
        {
            string query = "insert into \"Badge\" (id, name, description, user_id, created_at) values (@id, @name, @desc, @user_id, @created_at)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", badge.ID);
                cmd.Parameters.AddWithValue("name", badge.Name);
                cmd.Parameters.AddWithValue("desc", badge.Description);
                cmd.Parameters.AddWithValue("user_id", badge.UserId);
                cmd.Parameters.AddWithValue("created_at", badge.CreatedAt);
                cmd.ExecuteNonQuery();
            }
        }
    }


}