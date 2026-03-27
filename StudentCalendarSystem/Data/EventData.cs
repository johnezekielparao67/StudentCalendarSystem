using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StudentCalendar.Models;

namespace StudentCalendarSystem.Data
{
    public class EventData
    {
        private string filePath = "events.json";

        public void Save(List<Event> events)
        {
            string json = JsonSerializer.Serialize(events, new JsonSerializerOptions
            {
                WriteIndented = true 
            });

            File.WriteAllText(filePath, json);
        }

        public List<Event> Load()
        {
            if (!File.Exists(filePath))
                return new List<Event>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Event>>(json);
        }
    }
}