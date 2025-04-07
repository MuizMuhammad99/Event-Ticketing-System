using EventTicketingSystem.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventTicketingSystem.Mappings
{
    /// <summary>
    /// NHibernate mapping configuration for the Event entity.
    /// Maps Event properties to the Events database table.
    /// </summary>
    public class EventMap : ClassMapping<Event>
    {
        /// <summary>
        /// Initializes a new instance of the EventMap class and configures all property mappings.
        /// </summary>
        public EventMap()
        {
            // Configure the primary key mapping
            Id(x => x.Id, map =>
            {
                map.Column("Id");
                map.Type(NHibernate.NHibernateUtil.String);
                map.Generator(Generators.Assigned); // IDs are assigned by the application
            });

            // Configure Name property mapping
            Property(x => x.Name, map =>
            {
                map.Column("Name");
                map.NotNullable(true);
            });

            // Configure StartsOn property mapping
            Property(x => x.StartsOn, map =>
            {
                map.Column("StartsOn");
                map.NotNullable(true);
            });

            // Configure EndsOn property mapping
            Property(x => x.EndsOn, map =>
            {
                map.Column("EndsOn");
                map.NotNullable(true);
            });

            // Configure Location property mapping
            Property(x => x.Location, map =>
            {
                map.Column("Location");
                map.NotNullable(true);
            });

            // Specify the table name
            Table("Events");
        }
    }
}