"use client";

import { FaChartBar } from "react-icons/fa";
import {
  useTopEventsByCount,
  useTopEventsByRevenue,
} from "./_hooks/useSalesData";
import TopEventsByCount from "./_components/TopEventsByCount";
import TopEventsByRevenue from "./_components/TopEventsByRevenue";
import LoadingSpinner from "@/components/LoadingSpinner";
import ErrorAlert from "@/components/ErrorAlert";

/**
 * Sales summary page component
 *
 * Displays analytics for top-selling events by both ticket count and revenue
 * Shows loading indicators while data is being fetched
 * Handles error states with retry functionality
 */
export default function SalesSummaryPage() {
  // Fetch top events by ticket count
  const {
    events: topByCount,
    isLoading: isLoadingCount,
    error: errorCount,
    refetch: refetchCount,
  } = useTopEventsByCount();

  // Fetch top events by revenue
  const {
    events: topByRevenue,
    isLoading: isLoadingRevenue,
    error: errorRevenue,
    refetch: refetchRevenue,
  } = useTopEventsByRevenue();

  // Combined loading state
  const isLoading = isLoadingCount || isLoadingRevenue;

  return (
    <div className="space-y-6">
      <div className="flex items-center mb-6">
        <FaChartBar className="h-6 w-6 text-indigo-600 mr-2" />
        <h1 className="text-2xl font-bold text-gray-900">Sales Summary</h1>
      </div>

      {/* Error alerts */}
      {(errorCount || errorRevenue) && (
        <div className="space-y-4">
          {errorCount && (
            <ErrorAlert message={errorCount} onRetry={refetchCount} />
          )}
          {errorRevenue && (
            <ErrorAlert message={errorRevenue} onRetry={refetchRevenue} />
          )}
        </div>
      )}

      {/* Content */}
      {isLoading ? (
        <LoadingSpinner />
      ) : (
        <div className="grid md:grid-cols-2 gap-6">
          <TopEventsByCount events={topByCount} isLoading={false} />
          <TopEventsByRevenue events={topByRevenue} isLoading={false} />
        </div>
      )}
    </div>
  );
}
