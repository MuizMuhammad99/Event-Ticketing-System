using System.Collections.Generic;
using EventTicketingSystem.Models;

namespace EventTicketingSystem.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for ticket-related business operations.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Retrieves all tickets for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve tickets for.</param>
        /// <returns>A collection of TicketSale entities for the specified event.</returns>
        /// <exception cref="System.ArgumentException">Thrown when eventId is null or empty.</exception>
        IEnumerable<TicketSale> GetTicketsForEvent(string eventId);

        /// <summary>
        /// Retrieves top-selling events by the number of tickets sold.
        /// </summary>
        /// <param name="count">Maximum number of events to return (default: 5).</param>
        /// <returns>A collection of the top Event entities ranked by ticket sales count.</returns>
        /// <exception cref="System.ArgumentException">Thrown when count is less than or equal to zero.</exception>
        IEnumerable<Event> GetTopSellingEventsByCount(int count = 5);

        /// <summary>
        /// Retrieves top-selling events by total revenue generated.
        /// </summary>
        /// <param name="count">Maximum number of events to return (default: 5).</param>
        /// <returns>A collection of the top Event entities ranked by revenue.</returns>
        /// <exception cref="System.ArgumentException">Thrown when count is less than or equal to zero.</exception>
        IEnumerable<Event> GetTopSellingEventsByRevenue(int count = 5);
    }
}