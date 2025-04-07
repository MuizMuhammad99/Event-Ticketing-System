using System;
using System.Collections.Generic;
using EventTicketingSystem.Models;
using EventTicketingSystem.Repositories.Interfaces;
using EventTicketingSystem.Services.Interfaces;

namespace EventTicketingSystem.Services
{
    /// <summary>
    /// Service implementation for event-related business logic.
    /// Handles the application's event operations and applies business rules.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Initializes a new instance of the EventService class.
        /// </summary>
        /// <param name="eventRepository">Repository for accessing event data.</param>
        /// <exception cref="ArgumentNullException">Thrown when eventRepository is null.</exception>
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        /// <summary>
        /// Retrieves all events from the repository.
        /// </summary>
        /// <returns>A collection of all Event entities.</returns>
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll();
        }

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The event ID to retrieve.</param>
        /// <returns>The requested Event entity or null if not found.</returns>
        /// <exception cref="ArgumentException">Thrown when id is null or empty.</exception>
        public Event GetEventById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Event ID cannot be null or empty", nameof(id));

            return _eventRepository.GetById(id);
        }

        /// <summary>
        /// Retrieves upcoming events within the specified number of days from today.
        /// </summary>
        /// <param name="days">Number of days to look ahead (valid values: 30, 60, 180).</param>
        /// <returns>A collection of Event entities starting within the specified period.</returns>
        /// <exception cref="ArgumentException">Thrown when days is less than or equal to zero.</exception>
        /// <remarks>
        /// If an invalid days value is provided (not 30, 60, or 180), defaults to 30 days.
        /// </remarks>
        public IEnumerable<Event> GetUpcomingEvents(int days)
        {
            if (days <= 0)
                throw new ArgumentException("Days must be greater than zero", nameof(days));

            // Limit to valid values
            if (days != 30 && days != 60 && days != 180)
                days = 30;  // Default to 30 days if invalid

            return _eventRepository.GetUpcomingEvents(days);
        }
    }
}