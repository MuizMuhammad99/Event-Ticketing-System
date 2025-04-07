import Link from "next/link";
import { FaCalendarAlt, FaChartBar } from "react-icons/fa";

/**
 * Home page component
 *
 * Landing page that provides navigation to the main sections of the application
 * with visual cards for Events and Sales sections
 */
export default function Home() {
  return (
    <div className="min-h-[70vh] flex flex-col items-center justify-center">
      <h1 className="text-3xl font-bold text-center mb-8 text-gray-900">
        Welcome to the Event Ticketing System
      </h1>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 max-w-4xl">
        <Link
          href="/events"
          className="bg-white shadow-md rounded-lg p-6 flex flex-col items-center hover:shadow-lg transition-shadow"
        >
          <FaCalendarAlt className="text-indigo-600 text-5xl mb-4" />
          <h2 className="text-xl font-semibold mb-2 text-gray-800">
            View Events
          </h2>
          <p className="text-gray-600 text-center">
            Browse all upcoming events and view their details
          </p>
        </Link>

        <Link
          href="/sales"
          className="bg-white shadow-md rounded-lg p-6 flex flex-col items-center hover:shadow-lg transition-shadow"
        >
          <FaChartBar className="text-green-600 text-5xl mb-4" />
          <h2 className="text-xl font-semibold mb-2 text-gray-800">
            Sales Summary
          </h2>
          <p className="text-gray-600 text-center">
            View top selling events by ticket count and revenue
          </p>
        </Link>
      </div>
    </div>
  );
}
