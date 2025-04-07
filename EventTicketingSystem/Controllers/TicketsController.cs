using System;
using System.Linq;
using EventTicketingSystem.DTOs;
using EventTicketingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventTicketingSystem.Controllers
{
    /// <summary>
    /// Handles API requests related to tickets.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        /// <summary>
        /// Initializes a new instance of the TicketsController.
        /// </summary>
        /// <param name="ticketService">Service for handling ticket operations.</param>
        /// <param name="logger">Logger for ticket-related operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when ticketService or logger is null.</exception>
        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all tickets for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve tickets for.</param>
        /// <returns>A list of ticket sales for the specified event.</returns>
        /// <response code="200">Returns the list of tickets.</response>
        /// <response code="500">If an error occurs during processing.</response>
        [HttpGet("event/{eventId}")]
        public IActionResult GetTicketsForEvent(string eventId)
        {
            try
            {
                var tickets = _ticketService.GetTicketsForEvent(eventId);

                var ticketDtos = tickets.Select(t => new TicketSaleDto
                {
                    Id = t.Id,
                    EventId = t.EventId,
                    UserId = t.UserId,
                    PurchaseDate = t.PurchaseDate,
                    Price = t.PriceInCents / 100m,  // Convert cents to dollars
                    EventName = t.Event?.Name ?? "Unknown Event"
                });

                return Ok(ticketDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets for event with ID {eventId}");
                return StatusCode(500, "An error occurred while retrieving tickets");
            }
        }
    }
}