"use client";

import { useState, useMemo } from "react";
import Link from "next/link";
import {
  FaSort,
  FaSortUp,
  FaSortDown,
  FaCalendar,
  FaMapMarkerAlt,
} from "react-icons/fa";
import { Event } from "@/types";
import { formatShortDate } from "@/utils/formatters";

/**
 * Props for the EventsTable component
 */
interface EventsTableProps {
  events: Event[];
}

/**
 * Defines sortable fields in the table
 */
type SortField = "name" | "startsOn";

/**
 * Defines sort directions
 */
type SortDirection = "asc" | "desc";

/**
 * Displays a sortable table of events with details and links
 * Supports sorting by name or date in ascending or descending order
 */
export default function EventsTable({ events }: EventsTableProps) {
  const [sortField, setSortField] = useState<SortField>("startsOn");
  const [sortDirection, setSortDirection] = useState<SortDirection>("asc");

  /**
   * Handles column header clicks to change sort order
   */
  const handleSort = (field: SortField) => {
    if (field === sortField) {
      setSortDirection(sortDirection === "asc" ? "desc" : "asc");
    } else {
      setSortField(field);
      setSortDirection("asc");
    }
  };

  /**
   * Returns the appropriate sort icon based on current sort state
   */
  const getSortIcon = (field: SortField) => {
    if (field !== sortField) return <FaSort className="ml-1 text-gray-400" />;
    return sortDirection === "asc" ? (
      <FaSortUp className="ml-1 text-indigo-600" />
    ) : (
      <FaSortDown className="ml-1 text-indigo-600" />
    );
  };

  /**
   * Memoized sorted events based on current sort field and direction
   */
  const sortedEvents = useMemo(() => {
    return [...events].sort((a, b) => {
      if (sortField === "name") {
        return sortDirection === "asc"
          ? a.name.localeCompare(b.name)
          : b.name.localeCompare(a.name);
      } else {
        // Sort by date
        const dateA = new Date(a.startsOn).getTime();
        const dateB = new Date(b.startsOn).getTime();
        return sortDirection === "asc" ? dateA - dateB : dateB - dateA;
      }
    });
  }, [events, sortField, sortDirection]);

  if (events.length === 0) {
    return (
      <div className="bg-white shadow-md rounded-lg p-8 text-center">
        <p className="text-gray-500">No upcoming events found.</p>
      </div>
    );
  }

  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th
                scope="col"
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                onClick={() => handleSort("name")}
              >
                <div className="flex items-center">
                  Event Name
                  {getSortIcon("name")}
                </div>
              </th>
              <th
                scope="col"
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                onClick={() => handleSort("startsOn")}
              >
                <div className="flex items-center">
                  Date
                  {getSortIcon("startsOn")}
                </div>
              </th>
              <th
                scope="col"
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Location
              </th>
              <th
                scope="col"
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Actions
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {sortedEvents.map((event) => (
              <tr key={event.id} className="hover:bg-gray-50">
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="text-sm font-medium text-gray-900">
                    {event.name}
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center text-sm text-gray-500">
                    <FaCalendar className="mr-2 text-indigo-500" />
                    <span>
                      {formatShortDate(event.startsOn)}
                      {" - "}
                      {formatShortDate(event.endsOn)}
                    </span>
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center text-sm text-gray-500">
                    <FaMapMarkerAlt className="mr-2 text-indigo-500" />
                    {event.location}
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                  <Link
                    href={`/events/${event.id}`}
                    className="text-indigo-600 hover:text-indigo-900"
                  >
                    View Details
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
