using System.Reflection;
using EventTicketingSystem.Mappings;
using EventTicketingSystem.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
// Explicitly using NHibernate's ISession to avoid ambiguity
using ISession = NHibernate.ISession;

namespace EventTicketingSystem.Infrastructure
{
    /// <summary>
    /// Static helper class for NHibernate session management and configuration.
    /// Provides centralized access to the session factory and database sessions.
    /// </summary>
    public static class NHibernateHelper
    {
        /// <summary>
        /// Singleton instance of the NHibernate session factory.
        /// </summary>
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Gets or initializes the NHibernate session factory using the provided connection string.
        /// Configures NHibernate to use SQLite with specific settings.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>An initialized ISessionFactory instance.</returns>
        public static ISessionFactory GetSessionFactory(string connectionString)
        {
            if (_sessionFactory == null)
            {
                var configuration = new Configuration();

                // Configure the database connection
                configuration.DataBaseIntegration(db =>
                {
                    db.ConnectionString = connectionString;
                    db.Driver<SQLite20Driver>();
                    db.Dialect<SQLiteDialect>();
                    db.LogSqlInConsole = true;
                    db.AutoCommentSql = true;
                });

                // Add entity mappings from the assembly
                var modelMapper = new ModelMapper();
                modelMapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

                configuration.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

                _sessionFactory = configuration.BuildSessionFactory();
            }

            return _sessionFactory;
        }

        /// <summary>
        /// Opens a new NHibernate session using the provided connection string.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>An open ISession instance ready for database operations.</returns>
        public static ISession OpenSession(string connectionString)
        {
            return GetSessionFactory(connectionString).OpenSession();
        }
    }
}