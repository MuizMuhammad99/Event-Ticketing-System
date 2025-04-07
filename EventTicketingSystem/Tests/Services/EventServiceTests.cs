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
    /// Unit tests for the EventService class.
    /// </summary>
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private EventService _eventService;

        /// <summary>
        /// Set up the test environment before each test.
        /// Creates mock repository and service instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _eventService = new EventService(_mockEventRepository.Object);
        }

        /// <summary>
        /// Tests that GetAllEvents returns all events from the repository.
        /// </summary>
        [Test]
        public void GetAllEvents_ReturnsAllEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1", StartsOn = DateTime.Now.AddDays(5), EndsOn = DateTime.Now.AddDays(6), Location = "Location 1" },
                new Event { Id = "2", Name = "Event 2", StartsOn = DateTime.Now.AddDays(10), EndsOn = DateTime.Now.AddDays(11), Location = "Location 2" }
            };

            _mockEventRepository.Setup(repo => repo.GetAll()).Returns(events);

            // Act
            var result = _eventService.GetAllEvents();

            // Assert
            Assert.That(result, Is.EqualTo(events));
            _mockEventRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests that GetEventById returns the correct event when given a valid ID.
        /// </summary>
        [Test]
        public void GetEventById_ValidId_ReturnsEvent()
        {
            // Arrange
            var eventId = "1";
            var expectedEvent = new Event { Id = eventId, Name = "Event 1", StartsOn = DateTime.Now.AddDays(5), EndsOn = DateTime.Now.AddDays(6), Location = "Location 1" };

            _mockEventRepository.Setup(repo => repo.GetById(eventId)).Returns(expectedEvent);

            // Act
            var result = _eventService.GetEventById(eventId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedEvent));
            _mockEventRepository.Verify(repo => repo.GetById(eventId), Times.Once);
        }

        /// <summary>
        /// Tests that GetEventById throws ArgumentException when given null or empty ID.
        /// </summary>
        [Test]
        public void GetEventById_NullOrEmptyId_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _eventService.GetEventById(null));
            Assert.Throws<ArgumentException>(() => _eventService.GetEventById(string.Empty));
        }

        /// <summary>
        /// Tests that GetUpcomingEvents returns events for valid day parameters.
        /// </summary>
        [Test]
        public void GetUpcomingEvents_ValidDays_ReturnsEvents()
        {
            // Arrange
            var days = 30;
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1", StartsOn = DateTime.Now.AddDays(5), EndsOn = DateTime.Now.AddDays(6), Location = "Location 1" },
                new Event { Id = "2", Name = "Event 2", StartsOn = DateTime.Now.AddDays(10), EndsOn = DateTime.Now.AddDays(11), Location = "Location 2" }
            };

            _mockEventRepository.Setup(repo => repo.GetUpcomingEvents(days)).Returns(events);

            // Act
            var result = _eventService.GetUpcomingEvents(days);

            // Assert
            Assert.That(result, Is.EqualTo(events));
            _mockEventRepository.Verify(repo => repo.GetUpcomingEvents(days), Times.Once);
        }

        /// <summary>
        /// Tests that GetUpcomingEvents throws ArgumentException for non-positive day values.
        /// </summary>
        [Test]
        public void GetUpcomingEvents_InvalidDays_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _eventService.GetUpcomingEvents(0));
            Assert.Throws<ArgumentException>(() => _eventService.GetUpcomingEvents(-1));
        }

        /// <summary>
        /// Tests that GetUpcomingEvents defaults to 30 days when given a non-standard days value.
        /// </summary>
        [Test]
        public void GetUpcomingEvents_NonStandardDays_UsesDefaultValue()
        {
            // Arrange
            var invalidDays = 45; // Not 30, 60, or 180
            var defaultDays = 30;
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1", StartsOn = DateTime.Now.AddDays(5), EndsOn = DateTime.Now.AddDays(6), Location = "Location 1" }
            };

            _mockEventRepository.Setup(repo => repo.GetUpcomingEvents(defaultDays)).Returns(events);

            // Act
            var result = _eventService.GetUpcomingEvents(invalidDays);

            // Assert
            Assert.That(result, Is.EqualTo(events));
            _mockEventRepository.Verify(repo => repo.GetUpcomingEvents(defaultDays), Times.Once);
        }
    }
}