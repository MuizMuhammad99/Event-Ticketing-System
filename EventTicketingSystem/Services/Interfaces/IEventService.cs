using System.Collections.Generic;
using EventTicketingSystem.Models;

namespace EventTicketingSystem.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for event-related business operations.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves all events from the system.
        /// </summary>
        /// <returns>A collection of all Event entities.</returns>
        IEnumerable<Event> GetAllEvents();

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The event ID to retrieve.</param>
        /// <returns>The requested Event entity or null if not found.</returns>
        /// <exception cref="System.ArgumentException">Thrown when id is null or empty.</exception>
        Event GetEventById(string id);

        /// <summary>
        /// Retrieves upcoming events within the specified number of days from today.
        /// </summary>
        /// <param name="days">Number of days to look ahead (valid values: 30, 60, 180).</param>
        /// <returns>A collection of Event entities starting within the specified period.</returns>
        /// <exception cref="System.ArgumentException">Thrown when days is less than or equal to zero.</exception>
        /// <remarks>
        /// If an invalid days value is provided (not 30, 60, or 180), defaults to 30 days.
        /// </remarks>
        IEnumerable<Event> GetUpcomingEvents(int days);
    }
}