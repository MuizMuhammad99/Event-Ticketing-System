using EventTicketingSystem.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EventTicketingSystem.Mappings
{
    /// <summary>
    /// NHibernate mapping configuration for the TicketSale entity.
    /// Maps TicketSale properties to the TicketSales database table.
    /// </summary>
    public class TicketSaleMap : ClassMapping<TicketSale>
    {
        /// <summary>
        /// Initializes a new instance of the TicketSaleMap class and configures all property mappings.
        /// </summary>
        public TicketSaleMap()
        {
            // Configure the primary key mapping
            Id(x => x.Id, map =>
            {
                map.Column("Id");
                map.Type(NHibernate.NHibernateUtil.String);
                map.Generator(Generators.Assigned); // IDs are assigned by the application
            });

            // Configure UserId property mapping
            Property(x => x.UserId, map =>
            {
                map.Column("UserId");
                map.NotNullable(true);
            });

            // Configure PurchaseDate property mapping
            Property(x => x.PurchaseDate, map =>
            {
                map.Column("PurchaseDate");
                map.NotNullable(true);
            });

            // Configure PriceInCents property mapping
            Property(x => x.PriceInCents, map =>
            {
                map.Column("PriceInCents");
                map.NotNullable(true);
            });

            // Configure the Many-to-One relationship with Event
            // This handles the EventId property through the navigation property
            ManyToOne(x => x.Event, map =>
            {
                map.Column("EventId");
                map.ForeignKey("FK_TicketSale_Event");
                map.Lazy(LazyRelation.NoLazy); // Eager loading of the related Event
                map.NotNullable(true);
            });

            // Specify the table name
            Table("TicketSales");
        }
    }
}