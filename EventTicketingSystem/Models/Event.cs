using System;

namespace EventTicketingSystem.Models
{
    /// <summary>
    /// Represents an event in the ticketing system.
    /// This is a persistent entity mapped to the Events table.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name or title of the event.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the event begins.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual DateTime StartsOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the event ends.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual DateTime EndsOn { get; set; }

        /// <summary>
        /// Gets or sets the physical location where the event takes place.
        /// The virtual modifier enables NHibernate proxying.
        /// </summary>
        public virtual string Location { get; set; }
    }
}