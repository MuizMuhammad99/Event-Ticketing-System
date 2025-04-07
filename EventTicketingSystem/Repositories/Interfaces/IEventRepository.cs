using System;
using System.Collections.Generic;
using EventTicketingSystem.Models;

namespace EventTicketingSystem.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for accessing Event data in the repository.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Retrieves all events from the repository.
        /// </summary>
        /// <returns>A collection of all Event entities.</returns>
        IEnumerable<Event> GetAll();

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The event ID to retrieve.</param>
        /// <returns>The requested Event entity or null if not found.</returns>
        Event GetById(string id);

        /// <summary>
        /// Retrieves upcoming events within the specified number of days from today.
        /// </summary>
        /// <param name="daysAhead">Number of days to look ahead from the current date.</param>
        /// <returns>A collection of Event entities starting within the specified period.</returns>
        IEnumerable<Event> GetUpcomingEvents(int daysAhead);

        /// <summary>
        /// Retrieves top-selling events by the number of tickets sold.
        /// </summary>
        /// <param name="count">Maximum number of events to return.</param>
        /// <returns>A collection of the top Event entities ranked by ticket sales count.</returns>
        IEnumerable<Event> GetTopSellingEventsByCount(int count);

        /// <summary>
        /// Retrieves top-selling events by total revenue generated.
        /// </summary>
        /// <param name="count">Maximum number of events to return.</param>
        /// <returns>A collection of the top Event entities ranked by revenue.</returns>
        IEnumerable<Event> GetTopSellingEventsByRevenue(int count);
    }
}