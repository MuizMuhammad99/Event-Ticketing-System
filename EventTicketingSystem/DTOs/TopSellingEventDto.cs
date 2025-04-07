namespace EventTicketingSystem.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a top-selling event with sales metrics.
    /// Used for API responses when returning analytics about event performance.
    /// </summary>
    public class TopSellingEventDto
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
        /// The physical location where the event takes place.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The total number of tickets sold for this event.
        /// </summary>
        public int TicketCount { get; set; }

        /// <summary>
        /// The total revenue generated from ticket sales for this event.
        /// </summary>
        public decimal TotalRevenue { get; set; }
    }
}