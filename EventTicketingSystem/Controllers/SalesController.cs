// SalesController.cs
using System;
using System.Linq;
using EventTicketingSystem.DTOs;
using EventTicketingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventTicketingSystem.Controllers
{
    /// <summary>
    /// Handles API requests related to sales analytics.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<SalesController> _logger;

        /// <summary>
        /// Initializes a new instance of the SalesController.
        /// </summary>
        /// <param name="ticketService">Service for handling ticket operations.</param>
        /// <param name="logger">Logger for sales-related operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when ticketService or logger is null.</exception>
        public SalesController(ITicketService ticketService, ILogger<SalesController> logger)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets top-selling events by ticket count.
        /// </summary>
        /// <param name="count">Number of events to return (default: 5, max: 100)</param>
        /// <returns>A list of top-selling events.</returns>
        /// <response code="200">Returns the list of top-selling events.</response>
        /// <response code="500">If an error occurs during processing.</response>
        [HttpGet("top-by-count")]
        public IActionResult GetTopSellingEventsByCount([FromQuery] int count = 5)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    _logger.LogWarning($"Invalid count parameter: {count}. Defaulting to 5.");
                    count = 5;
                }

                var events = _ticketService.GetTopSellingEventsByCount(count);

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
                _logger.LogError(ex, "Error getting top selling events by count");
                return StatusCode(500, "An error occurred while retrieving top selling events");
            }
        }

        /// <summary>
        /// Gets top-selling events by revenue.
        /// </summary>
        /// <param name="count">Number of events to return (default: 5, max: 100)</param>
        /// <returns>A list of top-selling events by revenue.</returns>
        /// <response code="200">Returns the list of top-selling events.</response>
        /// <response code="500">If an error occurs during processing.</response>
        [HttpGet("top-by-revenue")]
        public IActionResult GetTopSellingEventsByRevenue([FromQuery] int count = 5)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    _logger.LogWarning($"Invalid count parameter: {count}. Defaulting to 5.");
                    count = 5;
                }

                var events = _ticketService.GetTopSellingEventsByRevenue(count);

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
                _logger.LogError(ex, "Error getting top selling events by revenue");
                return StatusCode(500, "An error occurred while retrieving top selling events");
            }
        }
    }
}