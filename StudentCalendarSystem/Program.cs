using System;
using StudentCalendar.Services;
using StudentCalendar.Models;

namespace StudentCalendar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalendarService calendar = new CalendarService();

            while (true)
            {
                Console.WriteLine("Simple Student Calendar.");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("1. Add Event");
                Console.WriteLine("2. View Events");
                Console.WriteLine("3. Edit Event");
                Console.WriteLine("4. Delete Event");
                Console.WriteLine("5. Exit");
                Console.WriteLine("-----------------------------------");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();
  
                switch (choice)
                {
                    case "1":
                        calendar.AddEvent();
                        break;

                    case "2":
                        calendar.ViewEvents();
                        break;

                    case "3":
                        calendar.EditEvent();
                        break;

                    case "4":
                        calendar.DeleteEvent();
                        break;

                    case "5":
                        Console.WriteLine("Exiting program...");
                        Console.WriteLine("-----------------------------------");
                        return;
                      
                 

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}