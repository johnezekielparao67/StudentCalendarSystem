using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using StudentCalendar.Models;

namespace StudentCalendarSystem.Data
{
    public class EventDb
    {
        private string connectionString =
    "Server=localhost\\SQLEXPRESS;Database=StudentCalendarDB;Trusted_Connection=True;Encrypt=False;";

        public void Save(List<Event> events)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM Events";
                using var deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.ExecuteNonQuery();

                // Insert each event
                foreach (var e in events)
                {
                    string insertQuery = @"INSERT INTO Events (Name, Date, Time, Description) 
                                           VALUES (@Name, @Date, @Time, @Desc)";
                    using var cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Name", e.Name);
                    cmd.Parameters.AddWithValue("@Date", e.Date);
                    cmd.Parameters.AddWithValue("@Time", e.Time);
                    cmd.Parameters.AddWithValue("@Desc", string.IsNullOrEmpty(e.Description) ? (object)DBNull.Value : e.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Event> Load()
        {
            List<Event> events = new List<Event>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT Name, Date, Time, Description FROM Events";
                using var cmd = new SqlCommand(selectQuery, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    events.Add(new Event
                    {
                        Name = reader["Name"].ToString(),
                        Date = reader["Date"].ToString(),
                        Time = reader["Time"].ToString(),
                        Description = reader["Description"] == DBNull.Value ? "" : reader["Description"].ToString()
                    });
                }
            }

            return events;
        }
    }
}