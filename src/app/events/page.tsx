"use client";

import { useState } from "react";
import { useSearchParams, useRouter } from "next/navigation";
import { FaCalendarAlt } from "react-icons/fa";
import { useEvents } from "./_hooks/useEvents";
import DaysFilter from "./_components/DaysFilter";
import EventsTable from "./_components/EventsTable";
import LoadingSpinner from "@/components/LoadingSpinner";
import ErrorAlert from "@/components/ErrorAlert";
import { DaysFilterType } from "@/types";

/**
 * Main events page component
 *
 * Displays a list of upcoming events with filtering options
 * Maintains filter state in URL query parameters
 */
export default function EventsPage() {
  const searchParams = useSearchParams();
  const router = useRouter();

  // Get initial days filter from URL or default to 30
  const initialDays = Number(
    searchParams.get("days") || "30"
  ) as DaysFilterType;

  // Use custom hook to fetch and manage events data
  const { events, days, setDays, isLoading, error, refetch } =
    useEvents(initialDays);

  /**
   * Updates days filter and syncs with URL parameters
   */
  const handleDaysChange = (newDays: DaysFilterType) => {
    setDays(newDays);
    router.push(`/events?days=${newDays}`);
  };

  return (
    <div className="space-y-6">
      <div className="sm:flex sm:items-center sm:justify-between">
        <div className="flex items-center">
          <FaCalendarAlt className="h-6 w-6 text-indigo-600 mr-2" />
          <h1 className="text-2xl font-bold text-gray-900">Upcoming Events</h1>
        </div>
      </div>

      <DaysFilter selectedDays={days} onChange={handleDaysChange} />

      {error && <ErrorAlert message={error} onRetry={refetch} />}

      {isLoading ? <LoadingSpinner /> : <EventsTable events={events} />}
    </div>
  );
}
