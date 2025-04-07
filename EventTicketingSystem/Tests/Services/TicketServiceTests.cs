using System;
using System.Collections.Generic;
using System.Linq;
using EventTicketingSystem.Models;
using EventTicketingSystem.Repositories.Interfaces;
using EventTicketingSystem.Services;
using Moq;
using NUnit.Framework;

namespace EventTicketingSystem.Tests.Services
{
    /// <summary>
    /// Unit tests for the TicketService class.
    /// </summary>
    [TestFixture]
    public class TicketServiceTests
    {
        private Mock<ITicketSaleRepository> _mockTicketSaleRepository;
        private Mock<IEventRepository> _mockEventRepository;
        private TicketService _ticketService;

        /// <summary>
        /// Set up the test environment before each test.
        /// Creates mock repositories and service instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mockTicketSaleRepository = new Mock<ITicketSaleRepository>();
            _mockEventRepository = new Mock<IEventRepository>();
            _ticketService = new TicketService(_mockTicketSaleRepository.Object, _mockEventRepository.Object);
        }

        /// <summary>
        /// Tests that GetTicketsForEvent returns tickets when given a valid event ID.
        /// </summary>
        [Test]
        public void GetTicketsForEvent_ValidEventId_ReturnsTickets()
        {
            // Arrange
            var eventId = "1";
            var event1 = new Event { Id = eventId, Name = "Event 1" };
            var tickets = new List<TicketSale>
            {
                new TicketSale { Id = "T1", EventId = eventId, UserId = "U1", PurchaseDate = DateTime.Now, PriceInCents = 1000, Event = event1 },
                new TicketSale { Id = "T2", EventId = eventId, UserId = "U2", PurchaseDate = DateTime.Now, PriceInCents = 2000, Event = event1 }
            };

            _mockTicketSaleRepository.Setup(repo => repo.GetByEventId(eventId)).Returns(tickets);

            // Act
            var result = _ticketService.GetTicketsForEvent(eventId);

            // Assert
            Assert.That(result, Is.EqualTo(tickets));
            _mockTicketSaleRepository.Verify(repo => repo.GetByEventId(eventId), Times.Once);
        }

        /// <summary>
        /// Tests that GetTicketsForEvent throws ArgumentException when given null or empty event ID.
        /// </summary>
        [Test]
        public void GetTicketsForEvent_NullOrEmptyEventId_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ticketService.GetTicketsForEvent(null));
            Assert.Throws<ArgumentException>(() => _ticketService.GetTicketsForEvent(string.Empty));
        }

        /// <summary>
        /// Tests that GetTopSellingEventsByCount returns events when given a valid count.
        /// </summary>
        [Test]
        public void GetTopSellingEventsByCount_ValidCount_ReturnsEvents()
        {
            // Arrange
            var count = 5;
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1" },
                new Event { Id = "2", Name = "Event 2" }
            };

            _mockEventRepository.Setup(repo => repo.GetTopSellingEventsByCount(count)).Returns(events);

            // Act
            var result = _ticketService.GetTopSellingEventsByCount(count);

            // Assert
            Assert.That(result, Is.EqualTo(events));
            _mockEventRepository.Verify(repo => repo.GetTopSellingEventsByCount(count), Times.Once);
        }

        /// <summary>
        /// Tests that GetTopSellingEventsByCount throws ArgumentException for non-positive count values.
        /// </summary>
        [Test]
        public void GetTopSellingEventsByCount_InvalidCount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ticketService.GetTopSellingEventsByCount(0));
            Assert.Throws<ArgumentException>(() => _ticketService.GetTopSellingEventsByCount(-1));
        }

        /// <summary>
        /// Tests that GetTopSellingEventsByRevenue returns events when given a valid count.
        /// </summary>
        [Test]
        public void GetTopSellingEventsByRevenue_ValidCount_ReturnsEvents()
        {
            // Arrange
            var count = 5;
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1" },
                new Event { Id = "2", Name = "Event 2" }
            };

            _mockEventRepository.Setup(repo => repo.GetTopSellingEventsByRevenue(count)).Returns(events);

            // Act
            var result = _ticketService.GetTopSellingEventsByRevenue(count);

            // Assert
            Assert.That(result, Is.EqualTo(events));
            _mockEventRepository.Verify(repo => repo.GetTopSellingEventsByRevenue(count), Times.Once);
        }

        /// <summary>
        /// Tests that GetTopSellingEventsByRevenue throws ArgumentException for non-positive count values.
        /// </summary>
        [Test]
        public void GetTopSellingEventsByRevenue_InvalidCount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ticketService.GetTopSellingEventsByRevenue(0));
            Assert.Throws<ArgumentException>(() => _ticketService.GetTopSellingEventsByRevenue(-1));
        }
    }
}