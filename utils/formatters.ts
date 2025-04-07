import { format, formatDistance } from "date-fns";

/**
 * Formats a date string to full date and time (e.g., "January 1, 2024 12:00 PM")
 */
export const formatDate = (dateString: string): string => {
  return format(new Date(dateString), "MMMM d, yyyy h:mm a");
};

/**
 * Formats a date string to abbreviated date without time (e.g., "Jan 1, 2024")
 */
export const formatShortDate = (dateString: string): string => {
  return format(new Date(dateString), "MMM d, yyyy");
};

/**
 * Formats a date range, showing both dates if they differ, or single date with time range if same day
 * Same day: "January 1, 2024 12:00 PM - 2:00 PM"
 * Different days: "January 1, 2024 12:00 PM - January 2, 2024 12:00 PM"
 */
export const formatDateRange = (startDate: string, endDate: string): string => {
  const start = new Date(startDate);
  const end = new Date(endDate);

  if (start.toDateString() === end.toDateString()) {
    return `${format(start, "MMMM d, yyyy")} ${format(
      start,
      "h:mm a"
    )} - ${format(end, "h:mm a")}`;
  }

  return `${format(start, "MMMM d, yyyy h:mm a")} - ${format(
    end,
    "MMMM d, yyyy h:mm a"
  )}`;
};

/**
 * Returns human-readable duration between two dates (e.g., "2 hours", "3 days")
 * Uses date-fns formatDistance
 */
export const formatDuration = (startDate: string, endDate: string): string => {
  const start = new Date(startDate);
  const end = new Date(endDate);

  return formatDistance(start, end, { addSuffix: false });
};

/**
 * Formats a number as USD currency (e.g., "$10.00")
 */
export const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(amount);
};
