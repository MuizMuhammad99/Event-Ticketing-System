"use client";

import Link from "next/link";
import { FaDollarSign } from "react-icons/fa";
import { EventSalesDto } from "@/types";
import { formatCurrency } from "@/utils/formatters";

/**
 * Props for the TopEventsByRevenue component
 */
interface TopEventsByRevenueProps {
  events: EventSalesDto[];
  isLoading: boolean;
}

/**
 * Displays a card showing events ranked by revenue
 * Includes loading state and empty state handling
 */
export default function TopEventsByRevenue({
  events,
  isLoading,
}: TopEventsByRevenueProps) {
  // Skeleton loader while data is loading
  if (isLoading) {
    return (
      <div className="animate-pulse bg-white rounded-lg shadow-md p-6">
        <div className="h-8 bg-gray-200 rounded w-1/2 mb-4"></div>
        {[...Array(5)].map((_, i) => (
          <div key={i} className="mb-4">
            <div className="h-6 bg-gray-200 rounded w-full mb-2"></div>
            <div className="h-4 bg-gray-200 rounded w-3/4"></div>
          </div>
        ))}
      </div>
    );
  }

  // Empty state when no data is available
  if (!events || events.length === 0) {
    return (
      <div className="bg-white rounded-lg shadow-md p-6">
        <h2 className="text-xl font-semibold text-gray-800 mb-4">
          Top Events by Revenue
        </h2>
        <p className="text-gray-500">No revenue data available</p>
      </div>
    );
  }

  // Populated state with ranking
  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden">
      <div className="px-6 py-4 bg-green-50 border-b border-green-100">
        <h2 className="text-xl font-semibold text-green-800 flex items-center">
          <FaDollarSign className="mr-2" />
          Top Events by Revenue
        </h2>
      </div>
      <div>
        {events.map((event, index) => (
          <div
            key={event.id}
            className={`px-6 py-4 flex items-center justify-between ${
              index !== events.length - 1 ? "border-b border-gray-100" : ""
            }`}
          >
            <div className="flex items-center">
              <div
                className={`w-8 h-8 rounded-full flex items-center justify-center mr-4 ${
                  index === 0
                    ? "bg-yellow-100 text-yellow-700"
                    : index === 1
                    ? "bg-gray-100 text-gray-700"
                    : index === 2
                    ? "bg-amber-100 text-amber-700"
                    : "bg-green-50 text-green-600"
                }`}
              >
                {index + 1}
              </div>
              <div>
                <Link
                  href={`/events/${event.id}`}
                  className="text-gray-800 font-medium hover:text-green-600"
                >
                  {event.name}
                </Link>
                <p className="text-sm text-gray-500">{event.location}</p>
              </div>
            </div>
            <div className="font-semibold text-gray-700">
              {formatCurrency(event.totalRevenue || 0)}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
