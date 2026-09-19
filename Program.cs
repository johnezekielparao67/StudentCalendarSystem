using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using StudentCalendar.Services;
using StudentCalendar.Models;

namespace StudentCalendar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            EmailService emailService = new EmailService(configuration);

            CalendarService calendar = new CalendarService(emailService);

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