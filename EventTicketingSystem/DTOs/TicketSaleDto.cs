using System;

namespace EventTicketingSystem.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a ticket sale.
    /// Used for API responses when returning ticket purchase information.
    /// </summary>
    public class TicketSaleDto
    {
        /// <summary>
        /// The unique identifier for the ticket sale.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The identifier of the event this ticket is for.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// The identifier of the user who purchased the ticket.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The date and time when the ticket was purchased.
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// The price of the ticket in decimal currency (dollars).
        /// Converted from cents stored in the database.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The name of the event this ticket is for.
        /// </summary>
        public string EventName { get; set; }
    }
}