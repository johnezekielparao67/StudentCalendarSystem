using StudentCalendar.Models;
using StudentCalendarSystem.Data;
using System;
using System.Collections.Generic;

namespace StudentCalendar.Services
{
    public class CalendarService
    {
        EventData jsonData = new EventData();
        EventDb dbData = new EventDb();
        List<Event> events;

        public CalendarService()
        {

            events = jsonData.Load();
        }

        public void AddEvent()
        {
            Console.WriteLine("-----------------------------------");
            Console.Write("Enter event name: ");
            string name = Console.ReadLine();

            Console.Write("Enter event date (yyyy-mm-dd): ");
            string date = Console.ReadLine();

            Console.Write("Enter time: ");
            string time = Console.ReadLine();

            Console.Write("Enter event description: ");
            string description = Console.ReadLine();

            events.Add(new Event
            {
                Name = name,
                Date = date,
                Time = time,
                Description = string.IsNullOrEmpty(description) ? null : description
            });

            jsonData.Save(events);
            dbData.Save(events);

            Console.WriteLine("Event added successfully!");
            Console.WriteLine("-----------------------------------");
        }

        public void ViewEvents()
        {
            if (events.Count == 0)
            {
                Console.WriteLine("No events to display.");
                return;
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Events: ");
            Console.WriteLine("-----------------------------------");

            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + events[i].Name);
                Console.WriteLine("Date: " + events[i].Date);
                Console.WriteLine("Time: " + events[i].Time);
                Console.WriteLine("Description: " + (events[i].Description ?? ""));
                Console.WriteLine("-----------------------------------");
            }
        }

        public void EditEvent()
        {
            Console.WriteLine("-----------------------------------");

            if (events.Count == 0)
            {
                Console.WriteLine("No events to edit.");
                return;
            }

            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + events[i].Name);
            }

            Console.Write("Enter number: ");
            int editIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (editIndex >= 0 && editIndex < events.Count)
            {
                Console.Write("Enter new event name (leave blank to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) events[editIndex].Name = newName;

                Console.Write("Enter new date (yyyy-mm-dd) or leave blank: ");
                string newDate = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDate)) events[editIndex].Date = newDate;

                Console.Write("Enter new time (leave blank): ");
                string newTime = Console.ReadLine();
                if (!string.IsNullOrEmpty(newTime)) events[editIndex].Time = newTime;

                Console.Write("Enter new description (leave blank): ");
                string newDesc = Console.ReadLine();
                events[editIndex].Description = string.IsNullOrEmpty(newDesc) ? events[editIndex].Description : newDesc;
     
                jsonData.Save(events);
                dbData.Save(events);

                Console.WriteLine("Event updated!");
                Console.WriteLine("-----------------------------------");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }

        public void DeleteEvent()
        {
            Console.WriteLine("-----------------------------------");

            if (events.Count == 0)
            {
                Console.WriteLine("No events to delete.");
                return;
            }

            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + events[i].Name);
            }

            Console.Write("Enter number: ");
            int deleteIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (deleteIndex >= 0 && deleteIndex < events.Count)
            {
                events.RemoveAt(deleteIndex);

                jsonData.Save(events);
                dbData.Save(events);

                Console.WriteLine("Event deleted!");
                Console.WriteLine("-----------------------------------");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }
    }
}