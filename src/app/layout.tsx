import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import Navbar from "@/components/Navbar";

/**
 * Configure Inter font for consistent typography
 */
const inter = Inter({ subsets: ["latin"] });

/**
 * Global metadata for the application
 * Controls title, description in browser tabs and search results
 */
export const metadata: Metadata = {
  title: "Event Ticketing System",
  description: "Manage events and ticket sales",
};

/**
 * Root layout component
 * Wraps all pages with common elements like the navbar
 * and consistent styling/layout
 */
export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${inter.className} min-h-screen`}>
        <Navbar />
        <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          {children}
        </main>
      </body>
    </html>
  );
}
