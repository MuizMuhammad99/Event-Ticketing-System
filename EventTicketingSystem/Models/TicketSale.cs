using System;

namespace EventTicketingSystem.Models
{
    /// <summary>
    /// Represents a ticket sale transaction in the ticketing system.
    /// This is a persistent entity mapped to the TicketSales table.
    /// </summary>
    public class TicketSale
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ticket sale.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who purchased the ticket.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the ticket was purchased.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Gets or sets the price of the ticket in cents.
        /// Using cents avoids floating-point precision issues with currency.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual int PriceInCents { get; set; }

        /// <summary>
        /// Gets or sets the related Event entity.
        /// This is a navigation property for the Many-to-One relationship.
        /// The virtual modifier enables NHibernate proxying and lazy loading.
        /// </summary>
        public virtual Event Event { get; set; }

        /// <summary>
        /// Gets the ID of the related event.
        /// This property is managed by NHibernate through the Event navigation property.
        /// The setter is empty as the value is handled by NHibernate.
        /// </summary>
        public virtual string EventId
        {
            get { return Event?.Id; }
            set { /* This is handled by NHibernate */ }
        }
    }
}