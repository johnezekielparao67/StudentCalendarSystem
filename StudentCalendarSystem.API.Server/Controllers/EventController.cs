using Microsoft.AspNetCore.Mvc;
using StudentCalendar.Models;
using StudentCalendar.Services;

namespace StudentCalendarSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly CalendarApiService _calendarService;

        public EventsController()
        {
         
            _calendarService = new CalendarApiService();
        }

        // 1. GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_calendarService.GetAll());
        }

        // 2. GET BY NAME
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var studentEvent = _calendarService.GetByName(name);
            if (studentEvent == null) return NotFound(new { message = "Event not found." });

            return Ok(studentEvent);
        }

        // 3. POST (CREATE)
        [HttpPost]
        public IActionResult Create([FromBody] Event newEvent)
        {
            if (!_calendarService.AddEvent(newEvent, out string error))
            {
                return BadRequest(new { message = error });
            }

            return CreatedAtAction(nameof(GetByName), new { name = newEvent.Name }, newEvent);
        }

        // 4. PUT (UPDATE)
        [HttpPut("{currentName}")]
        public IActionResult Update(string currentName, [FromBody] Event updatedEvent)
        {
            if (!_calendarService.UpdateEvent(currentName, updatedEvent, out string error))
            {
                return BadRequest(new { message = error });
            }

            return NoContent();
        }

        // 5. DELETE
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var success = _calendarService.DeleteEvent(name);
            if (!success) return NotFound(new { message = "Event not found to delete." });

            return NoContent();
        }
    }
}