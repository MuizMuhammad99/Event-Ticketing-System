using System.Collections.Generic;
using EventTicketingSystem.Models;

namespace EventTicketingSystem.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for accessing TicketSale data in the repository.
    /// </summary>
    public interface ITicketSaleRepository
    {
        /// <summary>
        /// Retrieves all ticket sales from the repository.
        /// </summary>
        /// <returns>A collection of all TicketSale entities.</returns>
        IEnumerable<TicketSale> GetAll();

        /// <summary>
        /// Retrieves a specific ticket sale by its unique identifier.
        /// </summary>
        /// <param name="id">The ticket sale ID to retrieve.</param>
        /// <returns>The requested TicketSale entity or null if not found.</returns>
        TicketSale GetById(string id);

        /// <summary>
        /// Retrieves all ticket sales for a specific event.
        /// </summary>
        /// <param name="eventId">The event ID to retrieve tickets for.</param>
        /// <returns>A collection of TicketSale entities for the specified event.</returns>
        IEnumerable<TicketSale> GetByEventId(string eventId);
    }
}