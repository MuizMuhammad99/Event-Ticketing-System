"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { useParams } from "next/navigation";
import {
  FaArrowLeft,
  FaCalendarAlt,
  FaMapMarkerAlt,
  FaTicketAlt,
  FaUser,
  FaClock,
} from "react-icons/fa";
import { useEventDetails } from "../_hooks/useEvents";
import LoadingSpinner from "@/components/LoadingSpinner";
import ErrorAlert from "@/components/ErrorAlert";
import {
  formatDate,
  formatDateRange,
  formatDuration,
  formatCurrency,
} from "@/utils/formatters";
import { api } from "@/lib/api";
import { TicketSale } from "@/types";

export default function EventDetailsPage() {
  const { id } = useParams() as { id: string };
  const { event, isLoading, error, refetch } = useEventDetails(id);
  const [tickets, setTickets] = useState<TicketSale[]>([]);
  const [ticketsLoading, setTicketsLoading] = useState(false);
  const [ticketsError, setTicketsError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTickets = async () => {
      if (!id) return;

      setTicketsLoading(true);
      setTicketsError(null);

      try {
        const data = await api.tickets.getByEventId(id);
        setTickets(data);
      } catch (err) {
        console.error("Failed to fetch ticket sales:", err);
        setTicketsError("Failed to load ticket sales. Please try again later.");
      } finally {
        setTicketsLoading(false);
      }
    };

    if (event) {
      fetchTickets();
    }
  }, [id, event]);

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return (
      <div className="text-center py-10">
        <h2 className="text-xl font-semibold text-red-600">
          Error Loading Event
        </h2>
        <p className="mt-2 text-gray-500">
          {error.includes("not found")
            ? "The event you're looking for doesn't exist or has been removed."
            : error}
        </p>
        <ErrorAlert message={`Error: ${error}`} onRetry={refetch} />
        <Link
          href="/events"
          className="mt-4 inline-block px-4 py-2 bg-indigo-600 text-white rounded-md hover:bg-indigo-700 transition-colors"
        >
          Back to Events
        </Link>
      </div>
    );
  }

  if (!event) {
    return (
      <div className="text-center py-10">
        <h2 className="text-xl font-semibold text-gray-700">Event not found</h2>
        <p className="mt-2 text-gray-500">
          The event you're looking for doesn't exist or has been removed.
        </p>
        <Link
          href="/events"
          className="mt-4 inline-block px-4 py-2 bg-indigo-600 text-white rounded-md hover:bg-indigo-700 transition-colors"
        >
          Back to Events
        </Link>
      </div>
    );
  }

  const totalRevenue = tickets.reduce((sum, ticket) => sum + ticket.price, 0);

  return (
    <div className="space-y-6">
      <div className="flex items-center mb-6">
        <Link
          href="/events"
          className="text-indigo-600 hover:text-indigo-800 flex items-center mr-4"
        >
          <FaArrowLeft className="mr-1" /> Back to Events
        </Link>
        <h1 className="text-2xl font-bold text-gray-900 flex-grow">
          {event.name}
        </h1>
      </div>

      <div className="bg-white shadow-md rounded-lg overflow-hidden">
        <div className="p-6">
          <div className="grid md:grid-cols-2 gap-6">
            <div className="space-y-4">
              <div className="flex items-start">
                <FaCalendarAlt className="mt-1 mr-3 text-indigo-600" />
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Date & Time
                  </h3>
                  <p className="text-base text-gray-900">
                    {formatDateRange(event.startsOn, event.endsOn)}
                  </p>
                </div>
              </div>

              <div className="flex items-start">
                <FaClock className="mt-1 mr-3 text-indigo-600" />
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Duration
                  </h3>
                  <p className="text-base text-gray-900">
                    {formatDuration(event.startsOn, event.endsOn)}
                  </p>
                </div>
              </div>

              <div className="flex items-start">
                <FaMapMarkerAlt className="mt-1 mr-3 text-indigo-600" />
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Location
                  </h3>
                  <p className="text-base text-gray-900">{event.location}</p>
                </div>
              </div>
            </div>

            <div className="bg-indigo-50 p-4 rounded-md">
              <h3 className="text-lg font-medium text-indigo-800 mb-3">
                Sales Summary
              </h3>

              {ticketsLoading ? (
                <p className="text-gray-500">Loading ticket sales...</p>
              ) : ticketsError ? (
                <p className="text-red-500 text-sm">{ticketsError}</p>
              ) : (
                <div className="space-y-3">
                  <div className="flex justify-between items-center border-b border-indigo-100 pb-2">
                    <span className="text-sm text-gray-600">
                      Total Tickets Sold:
                    </span>
                    <span className="font-semibold text-indigo-700">
                      {tickets.length}
                    </span>
                  </div>
                  <div className="flex justify-between items-center">
                    <span className="text-sm text-gray-600">
                      Total Revenue:
                    </span>
                    <span className="font-semibold text-indigo-700">
                      {formatCurrency(totalRevenue)}
                    </span>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>

        {tickets.length > 0 && (
          <div className="px-6 py-4 border-t border-gray-200">
            <h3 className="text-lg font-medium text-gray-900 mb-4">
              Recent Ticket Sales
            </h3>
            <div className="overflow-x-auto">
              <table className="min-w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                  <tr>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                    >
                      Purchase Date
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                    >
                      User ID
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                    >
                      Price
                    </th>
                  </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                  {tickets.slice(0, 5).map((ticket) => (
                    <tr key={ticket.id}>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {formatDate(ticket.purchaseDate)}
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="flex items-center text-sm text-gray-500">
                          <FaUser className="mr-2 text-gray-400" />
                          {ticket.userId.substring(0, 8)}...
                        </div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                        {formatCurrency(ticket.price)}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            {tickets.length > 5 && (
              <div className="mt-4 text-right">
                <span className="text-sm text-gray-500">
                  Showing 5 of {tickets.length} tickets
                </span>
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
