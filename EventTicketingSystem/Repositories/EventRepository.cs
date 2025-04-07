using System;
using System.Collections.Generic;
using System.Linq;
using EventTicketingSystem.Infrastructure;
using EventTicketingSystem.Models;
using EventTicketingSystem.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace EventTicketingSystem.Repositories
{
    /// <summary>
    /// Repository implementation for accessing Event data using NHibernate.
    /// Provides methods for querying event information from the database.
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the EventRepository class.
        /// </summary>
        /// <param name="connectionString">Database connection string to use for all operations.</param>
        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Retrieves all events from the database.
        /// </summary>
        /// <returns>A collection of all Event entities.</returns>
        public IEnumerable<Event> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                return session.Query<Event>().ToList();
            }
        }

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The event ID to retrieve.</param>
        /// <returns>The requested Event entity or null if not found.</returns>
        public Event GetById(string id)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                return session.Get<Event>(id);
            }
        }

        /// <summary>
        /// Retrieves upcoming events within the specified number of days from today.
        /// </summary>
        /// <param name="daysAhead">Number of days to look ahead from the current date.</param>
        /// <returns>A collection of Event entities starting within the specified period, ordered by start date.</returns>
        public IEnumerable<Event> GetUpcomingEvents(int daysAhead)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                var today = DateTime.UtcNow;
                var endDate = today.AddDays(daysAhead);

                return session.Query<Event>()
                    .Where(e => e.StartsOn >= today && e.StartsOn <= endDate)
                    .OrderBy(e => e.StartsOn)
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves top-selling events by the number of tickets sold.
        /// </summary>
        /// <param name="count">Maximum number of events to return.</param>
        /// <returns>A collection of the top Event entities ranked by ticket sales count.</returns>
        public IEnumerable<Event> GetTopSellingEventsByCount(int count)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                // HQL query to get top events by ticket count
                var query = session.CreateQuery(@"
                    SELECT e, COUNT(ts.Id) as ticketCount 
                    FROM TicketSale ts
                    JOIN ts.Event e
                    GROUP BY e.Id 
                    ORDER BY ticketCount DESC");

                query.SetMaxResults(count);

                var results = query.List<object[]>();

                // Extract the Event objects from the results
                return results.Select(row => (Event)row[0]).ToList();
            }
        }

        /// <summary>
        /// Retrieves top-selling events by total revenue generated.
        /// </summary>
        /// <param name="count">Maximum number of events to return.</param>
        /// <returns>A collection of the top Event entities ranked by revenue.</returns>
        public IEnumerable<Event> GetTopSellingEventsByRevenue(int count)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                // HQL query to get top events by revenue
                var query = session.CreateQuery(@"
                    SELECT e, SUM(ts.PriceInCents) as revenue 
                    FROM TicketSale ts
                    JOIN ts.Event e
                    GROUP BY e.Id 
                    ORDER BY revenue DESC");

                query.SetMaxResults(count);

                var results = query.List<object[]>();

                // Extract the Event objects from the results
                return results.Select(row => (Event)row[0]).ToList();
            }
        }
    }
}