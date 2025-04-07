using System;

namespace EventTicketingSystem.DTOs
{
    /// <summary>
    /// Data Transfer Object representing an event.
    /// Used for API responses when returning event information.
    /// </summary>
    public class EventDto
    {
        /// <summary>
        /// The unique identifier for the event.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name or title of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date and time when the event begins.
        /// </summary>
        public DateTime StartsOn { get; set; }

        /// <summary>
        /// The date and time when the event ends.
        /// </summary>
        public DateTime EndsOn { get; set; }

        /// <summary>
        /// The physical location where the event takes place.
        /// </summary>
        public string Location { get; set; }
    }
}