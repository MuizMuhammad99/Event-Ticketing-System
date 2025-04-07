using System.Collections.Generic;
using System.Linq;
using EventTicketingSystem.Infrastructure;
using EventTicketingSystem.Models;
using EventTicketingSystem.Repositories.Interfaces;
using NHibernate.Linq;

namespace EventTicketingSystem.Repositories
{
    /// <summary>
    /// Repository implementation for accessing TicketSale data using NHibernate.
    /// Provides methods for querying ticket sale information from the database.
    /// </summary>
    public class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the TicketSaleRepository class.
        /// </summary>
        /// <param name="connectionString">Database connection string to use for all operations.</param>
        public TicketSaleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Retrieves all ticket sales from the database.
        /// </summary>
        /// <returns>A collection of all TicketSale entities.</returns>
        public IEnumerable<TicketSale> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                return session.Query<TicketSale>().ToList();
            }
        }

        /// <summary>
        /// Retrieves a specific ticket sale by its unique identifier.
        /// </summary>
        /// <param name="id">The ticket sale ID to retrieve.</param>
        /// <returns>The requested TicketSale entity or null if not found.</returns>
        public TicketSale GetById(string id)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                return session.Get<TicketSale>(id);
            }
        }

        /// <summary>
        /// Retrieves all ticket sales for a specific event, including the related Event data.
        /// </summary>
        /// <param name="eventId">The event ID to retrieve tickets for.</param>
        /// <returns>A collection of TicketSale entities for the specified event.</returns>
        /// <remarks>
        /// Uses HQL with JOIN FETCH to ensure proper eager loading of the Event entity.
        /// </remarks>
        public IEnumerable<TicketSale> GetByEventId(string eventId)
        {
            using (var session = NHibernateHelper.OpenSession(_connectionString))
            {
                // Using HQL instead of LINQ to ensure proper loading
                var query = session.CreateQuery(@"
                    FROM TicketSale ts 
                    JOIN FETCH ts.Event e
                    WHERE e.Id = :eventId");
                query.SetParameter("eventId", eventId);
                return query.List<TicketSale>();
            }
        }
    }
}