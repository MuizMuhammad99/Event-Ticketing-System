"use client";

import { useState, useEffect } from "react";
import { api } from "@/lib/api";
import { EventSalesDto } from "@/types";

/**
 * Custom hook for fetching top events by ticket count
 *
 * @param count - Number of top events to retrieve
 * @returns State and functions for top events by count
 */
export function useTopEventsByCount(count: number = 5) {
  const [events, setEvents] = useState<EventSalesDto[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetches top events by count and enhances with ticket count data
   */
  const fetchTopEvents = async () => {
    setIsLoading(true);
    setError(null);

    try {
      // First, get the top events by count
      const topEvents = await api.sales.getTopByCount(count);

      // For each event, fetch its ticket data to get the count
      const eventsWithTickets = await Promise.all(
        topEvents.map(async (event) => {
          try {
            const tickets = await api.tickets.getByEventId(event.id);
            return {
              ...event,
              ticketCount: tickets.length, // Add the ticket count
              totalRevenue: tickets.reduce(
                (sum, ticket) => sum + ticket.price,
                0
              ), // Calculate total revenue
            };
          } catch (err) {
            console.error(
              `Failed to fetch tickets for event ${event.id}:`,
              err
            );
            return {
              ...event,
              ticketCount: 0,
              totalRevenue: 0,
            };
          }
        })
      );

      setEvents(eventsWithTickets);
    } catch (err) {
      console.error("Failed to fetch top events by count:", err);
      setError(
        "Failed to load top events by ticket count. Please try again later."
      );
    } finally {
      setIsLoading(false);
    }
  };

  // Fetch data when count changes
  useEffect(() => {
    fetchTopEvents();
  }, [count]);

  return {
    events,
    isLoading,
    error,
    refetch: fetchTopEvents,
  };
}

/**
 * Custom hook for fetching top events by revenue
 *
 * @param count - Number of top events to retrieve
 * @returns State and functions for top events by revenue
 */
export function useTopEventsByRevenue(count: number = 5) {
  const [events, setEvents] = useState<EventSalesDto[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetches top events by revenue and enhances with revenue data
   */
  const fetchTopEvents = async () => {
    setIsLoading(true);
    setError(null);

    try {
      // First, get the top events by revenue
      const topEvents = await api.sales.getTopByRevenue(count);

      // For each event, fetch its ticket data to calculate revenue
      const eventsWithRevenue = await Promise.all(
        topEvents.map(async (event) => {
          try {
            const tickets = await api.tickets.getByEventId(event.id);
            return {
              ...event,
              ticketCount: tickets.length,
              totalRevenue: tickets.reduce(
                (sum, ticket) => sum + ticket.price,
                0
              ), // Calculate total revenue
            };
          } catch (err) {
            console.error(
              `Failed to fetch tickets for event ${event.id}:`,
              err
            );
            return {
              ...event,
              ticketCount: 0,
              totalRevenue: 0,
            };
          }
        })
      );

      setEvents(eventsWithRevenue);
    } catch (err) {
      console.error("Failed to fetch top events by revenue:", err);
      setError("Failed to load top events by revenue. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  };

  // Fetch data when count changes
  useEffect(() => {
    fetchTopEvents();
  }, [count]);

  return {
    events,
    isLoading,
    error,
    refetch: fetchTopEvents,
  };
}
