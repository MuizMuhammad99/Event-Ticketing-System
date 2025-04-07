// EventsController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using EventTicketingSystem.DTOs;
using EventTicketingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventTicketingSystem.Controllers
{
    /// <summary>
    /// Handles API requests related to events.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;

        /// <summary>
        /// Initializes a new instance of the EventsController.
        /// </summary>
        /// <param name="eventService">Service for handling event operations.</param>
        /// <param name="logger">Logger for event-related operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when eventService or logger is null.</exception>
        public EventsController(IEventService eventService, ILogger<EventsController> logger)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a list of upcoming events within the specified number of days.
        /// </summary>
        /// <param name="days">Number of days to look ahead (valid values: 30, 60, 180)</param>
        /// <returns>A list of upcoming events as EventDto objects.</returns>
        /// <response code="200">Returns the list of events.</response>
        /// <response code="500">If an error occurs during processing.</response>
        [HttpGet]
        public IActionResult GetUpcomingEvents([FromQuery] int days = 30)
        {
            try
            {
                if (days != 30 && days != 60 && days != 180)
                {
                    _logger.LogWarning($"Invalid days parameter: {days}. Defaulting to 30 days.");
                    days = 30;
                }

                var events = _eventService.GetUpcomingEvents(days);

                var eventDtos = events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartsOn = e.StartsOn,
                    EndsOn = e.EndsOn,
                    Location = e.Location
                });

                return Ok(eventDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting upcoming events");
                return StatusCode(500, "An error occurred while retrieving events");
            }
        }

        /// <summary>
        /// Gets an event by its unique identifier.
        /// </summary>
        /// <param name="id">The event ID to retrieve.</param>
        /// <returns>The requested event as an EventDto object.</returns>
        /// <response code="200">Returns the requested event.</response>
        /// <response code="404">If the event is not found.</response>
        /// <response code="500">If an error occurs during processing.</response>
        [HttpGet("{id}")]
        public IActionResult GetEventById(string id)
        {
            try
            {
                var evt = _eventService.GetEventById(id);

                if (evt == null)
                    return NotFound($"Event with ID {id} not found");

                var eventDto = new EventDto
                {
                    Id = evt.Id,
                    Name = evt.Name,
                    StartsOn = evt.StartsOn,
                    EndsOn = evt.EndsOn,
                    Location = evt.Location
                };

                return Ok(eventDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting event with ID {id}");
                return StatusCode(500, "An error occurred while retrieving the event");
            }
        }
    }
}