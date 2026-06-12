using StudentCalendar.Models;
using StudentCalendarSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentCalendar.Services
{
    public class CalendarApiService
    {
        private readonly EventData _jsonData = new EventData();
        private readonly EventDb _dbData = new EventDb();
        private List<Event> _events;

        public CalendarApiService()
        {
            _events = _jsonData.Load();
        }

        // 1. GET ALL 
        public List<Event> GetAll()
        {
            return _events;
        }

        // 2. GET BY NAME 
        public Event GetByName(string name)
        {
            return _events.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // 3. ADD EVENT 
        public bool AddEvent(Event newEvent, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(newEvent.Name))
            {
                errorMessage = "Name is required!";
                return false;
            }

            if (_events.Any(e => e.Date == newEvent.Date && e.Time == newEvent.Time))
            {
                errorMessage = "There is already an event at this time!";
                return false;
            }

            _events.Add(newEvent);
            _jsonData.Save(_events);
            _dbData.Save(_events);

            return true;
        }

        // 4. UPDATE EVENT 
        public bool UpdateEvent(string currentName, Event updatedModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            var existingEvent = _events.FirstOrDefault(e => e.Name.Equals(currentName, StringComparison.OrdinalIgnoreCase));

            if (existingEvent == null)
            {
                errorMessage = "Event not found.";
                return false;
            }

            if (_events.Any(e => e != existingEvent && e.Date == updatedModel.Date && e.Time == updatedModel.Time))
            {
                errorMessage = "There is already another event at this time!";
                return false;
            }

            existingEvent.Name = string.IsNullOrEmpty(updatedModel.Name) ? existingEvent.Name : updatedModel.Name;
            existingEvent.Date = string.IsNullOrEmpty(updatedModel.Date) ? existingEvent.Date : updatedModel.Date;
            existingEvent.Time = string.IsNullOrEmpty(updatedModel.Time) ? existingEvent.Time : updatedModel.Time;
            existingEvent.Description = string.IsNullOrEmpty(updatedModel.Description) ? existingEvent.Description : updatedModel.Description;

            _jsonData.Save(_events);
            _dbData.Save(_events);
            return true;
        }

        // 5. DELETE EVENT
        public bool DeleteEvent(string name)
        {
            var existingEvent = _events.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingEvent == null) return false;

            _events.Remove(existingEvent);
            _jsonData.Save(_events);
            _dbData.Save(_events);
            return true;
        }
    }
}