/**
 * Event type matching the backend EventDto
 * Contains basic event information returned from the API
 */
export interface Event {
  id: string;
  name: string;
  startsOn: string; // ISO date string
  endsOn: string; // ISO date string
  location: string;
}

/**
 * Ticket sale type matching the backend TicketSaleDto
 * Represents a ticket purchase record
 */
export interface TicketSale {
  id: string;
  eventId: string;
  userId: string;
  purchaseDate: string; // ISO date string
  price: number; // Already converted from cents to dollars in backend
  eventName: string;
}

/**
 * Event sales data for analytics dashboards
 * Used for displaying revenue and ticket count metrics
 */
export interface EventSalesDto {
  id: string;
  name: string;
  location: string;
  ticketCount?: number;
  totalRevenue?: number;
}

/**
 * Valid filter values for upcoming events endpoint
 * Matches the allowed values in EventsController
 */
export type DaysFilterType = 30 | 60 | 180;
