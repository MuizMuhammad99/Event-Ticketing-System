"use client";

import { FaExclamationTriangle } from "react-icons/fa";

/**
 * Props for the ErrorAlert component
 */
interface ErrorAlertProps {
  message: string;
  onRetry?: () => void;
}

/**
 * Displays an error message with optional retry functionality
 * Used to show API errors and other error states with consistent styling
 */
export default function ErrorAlert({ message, onRetry }: ErrorAlertProps) {
  return (
    <div className="bg-red-50 border-l-4 border-red-500 p-4 my-4 rounded-md shadow">
      <div className="flex">
        <div className="flex-shrink-0">
          <FaExclamationTriangle className="h-5 w-5 text-red-500" />
        </div>
        <div className="ml-3">
          <p className="text-sm text-red-700">{message}</p>
          {onRetry && (
            <div className="mt-2">
              <button
                type="button"
                onClick={onRetry}
                className="bg-red-100 hover:bg-red-200 text-red-800 font-semibold px-3 py-1 rounded text-xs transition-colors"
              >
                Try again
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
