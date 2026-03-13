using StudentCalendar.Models;
using System;

namespace StudentCalendar.Services
{
    public class CalendarService
    {
        Event[] events = new Event[100];
        int eventCount = 0;

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

            events[eventCount] = new Event
            {
                Name = name,
                Date = date,
                Time = time,
                Description = description
            };

            eventCount++;

            Console.WriteLine("Event added successfully!");
            Console.WriteLine("-----------------------------------");
        }



        public void ViewEvents()
        {
            if (eventCount == 0)
            {
                Console.WriteLine("No events to display.");
                return;
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Events: ");
            Console.WriteLine("-----------------------------------");

            for (int i = 0; i < eventCount; i++)
            {
         
                Console.WriteLine((i + 1) + ". " + events[i].Name);
                Console.WriteLine("Date: " + events[i].Date);
                Console.WriteLine("Time: " + events[i].Time);
                Console.WriteLine("Description: " + events[i].Description);
                Console.WriteLine("-----------------------------------");
            }
        }

        public void EditEvent()
        {
            Console.WriteLine("-----------------------------------");
            if (eventCount == 0)
            {
                Console.WriteLine("No events to edit.");
                return;
            }

            for (int i = 0; i < eventCount; i++)
            {
                Console.WriteLine((i + 1) + ". " + events[i].Name);
            }

            Console.Write("Enter number: ");
            int editIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (editIndex >= 0 && editIndex < eventCount)
            {
                Console.Write("Enter new event name (leave blank to keep current): ");
                string newName = Console.ReadLine();

                if (!string.IsNullOrEmpty(newName))
                    events[editIndex].Name = newName;

                Console.Write("Enter new date (yyyy - mm - dd) or (leave blank to keep current): ");
                string newDate = Console.ReadLine();

                if (!string.IsNullOrEmpty(newDate))
                    events[editIndex].Date = newDate;

                Console.Write("Enter new time (leave blank to keep current): ");
                string newTime = Console.ReadLine();

                if (!string.IsNullOrEmpty(newTime))
                    events[editIndex].Time = newTime;

                Console.Write("Enter new description (leave blank to keep current): ");
                string newDesc = Console.ReadLine();

                if (!string.IsNullOrEmpty(newDesc))
                    events[editIndex].Description = newDesc;

                Console.WriteLine("Event updated!");
                Console.WriteLine("-----------------------------------");
            }
        }

        public void DeleteEvent()
        {
            Console.WriteLine("-----------------------------------");
            if (eventCount == 0)
            {
                Console.WriteLine("No events to delete.");
                return;
            }

            for (int i = 0; i < eventCount; i++)
            {
                Console.WriteLine((i + 1) + ". " + events[i].Name);
            }

            Console.Write("Enter number: ");
            int deleteIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (deleteIndex >= 0 && deleteIndex < eventCount)
            {
                for (int i = deleteIndex; i < eventCount - 1; i++)
                {
                    events[i] = events[i + 1];
                }

                eventCount--;

                Console.WriteLine("Event deleted!");
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}