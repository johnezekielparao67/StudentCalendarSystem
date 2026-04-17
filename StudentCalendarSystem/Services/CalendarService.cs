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
        //ADD EVENT
        public void AddEvent()
        {
            Console.WriteLine("-----------------------------------");
            Console.Write("Enter event name: ");
            string name = Console.ReadLine();

            if (name == "")
            {
                Console.WriteLine("Name is required!");
                return;
            }

            DateTime tempDate;
            Console.Write("Enter event date (yyyy-mm-dd): ");
            string inputDate = Console.ReadLine();

            if (!DateTime.TryParse(inputDate, out tempDate))
            {
                Console.WriteLine("Invalid date!");
                return;
            }

            string date = tempDate.ToString("yyyy-MM-dd");

            DateTime tempDateTime;

            //ADD TIME
            Console.Write("Enter time (e.g. 2:00 PM or 14:00): ");
            string inputTime = Console.ReadLine();

            if (!DateTime.TryParse(inputTime, out tempDateTime))
            {
                Console.WriteLine("Invalid time!");
                return;
            }

            string time = tempDateTime.ToString("HH:mm");

            Console.Write("Enter event description: ");
            string description = Console.ReadLine();

            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].Date == date && events[i].Time == time)
                {
                    Console.WriteLine("There is already an event at this time!");
                    return;
                }
            }

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
            int editIndex;
            if (!int.TryParse(Console.ReadLine(), out editIndex))
            {
                Console.WriteLine("Invalid input!");
                return;
            }
            editIndex--;

            if (editIndex >= 0 && editIndex < events.Count)
            {
                Console.Write("Enter new event name (leave blank to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) events[editIndex].Name = newName;

                Console.Write("Enter new date (yyyy-mm-dd) or leave blank: ");
                string newDate = Console.ReadLine();

                if (!string.IsNullOrEmpty(newDate))
                {
                    DateTime tempDate;
                    if (!DateTime.TryParse(newDate, out tempDate))
                    {
                        Console.WriteLine("Invalid date!");
                        return;
                    }
                    events[editIndex].Date = tempDate.ToString("yyyy-MM-dd");
                }

                Console.Write("Enter new time (leave blank): ");
                string newTime = Console.ReadLine();

                if (!string.IsNullOrEmpty(newTime))
                {
                    TimeSpan tempTime;
                    if (!TimeSpan.TryParse(newTime, out tempTime))
                    {
                        Console.WriteLine("Invalid time!");
                        return;
                    }
                    events[editIndex].Time = tempTime.ToString(@"hh\:mm");
                }

                Console.Write("Enter new description (leave blank): ");
                string newDesc = Console.ReadLine();
                events[editIndex].Description = string.IsNullOrEmpty(newDesc) ? events[editIndex].Description : newDesc;

                for (int i = 0; i < events.Count; i++)
                {
                    if (i != editIndex &&
                        events[i].Date == events[editIndex].Date &&
                        events[i].Time == events[editIndex].Time)
                    {
                        Console.WriteLine("There is already an event at this time!");
                        return;
                    }
                }

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
            int deleteIndex;

            if (!int.TryParse(Console.ReadLine(), out deleteIndex))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            deleteIndex--;

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