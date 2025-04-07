/**
 * LoadingSpinner component
 *
 * A simple, accessible loading indicator that displays a spinning animation
 * Used throughout the application to indicate data loading states
 *
 * Features:
 * - Animated spinner using Tailwind's animate-spin
 * - Centered layout with proper spacing
 * - Includes screen reader text for accessibility
 */
export default function LoadingSpinner() {
  return (
    <div className="flex justify-center items-center py-10">
      <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-indigo-500"></div>
      <span className="sr-only">Loading...</span>
    </div>
  );
}
