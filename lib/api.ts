import axios from "axios";
import { Event, TicketSale, EventSalesDto } from "@/types";

/**
 * Base URL for the Event Ticketing System API
 */
const API_BASE_URL = "http://localhost:5134/api";

/**
 * Preconfigured axios instance with common configuration
 */
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

/**
 * Global error handling for API requests
 * Logs errors to console before rejecting the promise
 */
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("API Error:", error);
    return Promise.reject(error);
  }
);

/**
 * API client for interacting with the Event Ticketing System backend
 * Organized by resource type (events, tickets, sales)
 */
export const api = {
  // Event endpoints
  events: {
    /**
     * Fetches upcoming events within the specified time period
     * @param days Number of days ahead to fetch (30, 60, or 180)
     * @returns Promise containing an array of events
     */
    getUpcoming: async (days: 30 | 60 | 180 = 30): Promise<Event[]> => {
      const response = await apiClient.get(`/events?days=${days}`);
      return response.data;
    },

    /**
     * Fetches a specific event by its ID
     * @param id Event identifier
     * @returns Promise containing the event details
     */
    getById: async (id: string): Promise<Event> => {
      const response = await apiClient.get(`/events/${id}`);
      return response.data;
    },
  },

  // Ticket sales endpoints
  tickets: {
    /**
     * Fetches all ticket sales for a specific event
     * @param eventId Event identifier
     * @returns Promise containing an array of ticket sales
     */
    getByEventId: async (eventId: string): Promise<TicketSale[]> => {
      const response = await apiClient.get(`/tickets/event/${eventId}`);
      return response.data;
    },
  },

  // Sales analytics endpoints
  sales: {
    /**
     * Fetches top selling events by ticket count
     * @param count Number of top events to return (default: 5)
     * @returns Promise containing an array of events with sales metrics
     */
    getTopByCount: async (count: number = 5): Promise<EventSalesDto[]> => {
      const response = await apiClient.get(
        `/sales/top-by-count?count=${count}`
      );
      return response.data;
    },

    /**
     * Fetches top selling events by revenue
     * @param count Number of top events to return (default: 5)
     * @returns Promise containing an array of events with sales metrics
     */
    getTopByRevenue: async (count: number = 5): Promise<EventSalesDto[]> => {
      const response = await apiClient.get(
        `/sales/top-by-revenue?count=${count}`
      );
      return response.data;
    },
  },
};
