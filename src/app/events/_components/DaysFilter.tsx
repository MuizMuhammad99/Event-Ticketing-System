"use client";

import { DaysFilterType } from "@/types";

/**
 * Props for the DaysFilter component
 */
interface DaysFilterProps {
  selectedDays: DaysFilterType;
  onChange: (days: DaysFilterType) => void;
}

/**
 * Filter component for selecting the time range for upcoming events
 * Provides buttons for predefined time periods (30, 60, 180 days)
 */
export default function DaysFilter({
  selectedDays,
  onChange,
}: DaysFilterProps) {
  const options: DaysFilterType[] = [30, 60, 180];

  return (
    <div className="bg-white rounded-md shadow-sm p-4 mb-6">
      <h3 className="text-sm font-medium text-gray-500 mb-2">
        Show events for the next:
      </h3>
      <div className="flex space-x-2">
        {options.map((days) => (
          <button
            key={days}
            onClick={() => onChange(days)}
            className={`px-4 py-2 rounded-md text-sm font-medium transition-colors ${
              selectedDays === days
                ? "bg-indigo-600 text-white"
                : "bg-gray-100 text-gray-700 hover:bg-gray-200"
            }`}
          >
            {days} days
          </button>
        ))}
      </div>
    </div>
  );
}
