using System;
using System.Collections.Generic;
using EventTicketingSystem.Models;
using EventTicketingSystem.Repositories.Interfaces;
using EventTicketingSystem.Services.Interfaces;

namespace EventTicketingSystem.Services
{
    /// <summary>
    /// Service implementation for ticket-related business logic.
    /// Handles ticket sales operations and analytics.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Initializes a new instance of the TicketService class.
        /// </summary>
        /// <param name="ticketSaleRepository">Repository for accessing ticket sale data.</param>
        /// <param name="eventRepository">Repository for accessing event data.</param>
        /// <exception cref="ArgumentNullException">Thrown when ticketSaleRepository or eventRepository is null.</exception>
        public TicketService(ITicketSaleRepository ticketSaleRepository, IEventRepository eventRepository)
        {
            _ticketSaleRepository = ticketSaleRepository ?? throw new ArgumentNullException(nameof(ticketSaleRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        /// <summary>
        /// Retrieves all tickets for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve tickets for.</param>
        /// <returns>A collection of TicketSale entities for the specified event.</returns>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        public IEnumerable<TicketSale> GetTicketsForEvent(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
                throw new ArgumentException("Event ID cannot be null or empty", nameof(eventId));

            return _ticketSaleRepository.GetByEventId(eventId);
        }

        /// <summary>
        /// Retrieves top-selling events by the number of tickets sold.
        /// </summary>
        /// <param name="count">Maximum number of events to return (default: 5).</param>
        /// <returns>A collection of the top Event entities ranked by ticket sales count.</returns>
        /// <exception cref="ArgumentException">Thrown when count is less than or equal to zero.</exception>
        public IEnumerable<Event> GetTopSellingEventsByCount(int count = 5)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero", nameof(count));

            return _eventRepository.GetTopSellingEventsByCount(count);
        }

        /// <summary>
        /// Retrieves top-selling events by total revenue generated.
        /// </summary>
        /// <param name="count">Maximum number of events to return (default: 5).</param>
        /// <returns>A collection of the top Event entities ranked by revenue.</returns>
        /// <exception cref="ArgumentException">Thrown when count is less than or equal to zero.</exception>
        public IEnumerable<Event> GetTopSellingEventsByRevenue(int count = 5)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero", nameof(count));

            return _eventRepository.GetTopSellingEventsByRevenue(count);
        }
    }
}