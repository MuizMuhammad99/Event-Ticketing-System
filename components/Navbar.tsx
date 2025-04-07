"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { CalendarDays, BarChart3 } from "lucide-react";
import { cva } from "class-variance-authority";

/**
 * Defines styling for navigation links with active and inactive states
 * Uses class-variance-authority for conditional styling
 */
const navLinkStyles = cva(
  "flex items-center gap-2 px-4 py-2 rounded-md font-medium transition-all duration-200",
  {
    variants: {
      active: {
        true: "text-indigo-600 bg-indigo-50/80",
        false: "text-gray-600 hover:text-indigo-600 hover:bg-gray-50/80",
      },
    },
    defaultVariants: {
      active: false,
    },
  }
);

/**
 * Defines styling for navigation icons with active and inactive states
 */
const iconStyles = cva("w-4 h-4", {
  variants: {
    active: {
      true: "text-indigo-500",
      false: "text-gray-400 group-hover:text-indigo-500",
    },
  },
  defaultVariants: {
    active: false,
  },
});

/**
 * Type definition for navigation items
 */
type NavItem = {
  path: string;
  label: string;
  icon: React.ElementType;
};

/**
 * Main navigation component
 * Renders the application header with logo and navigation links
 * Highlights the active section based on current URL path
 */
export default function Navbar() {
  const pathname = usePathname();

  // Define available navigation destinations
  const navItems: NavItem[] = [
    { path: "/events", label: "Events", icon: CalendarDays },
    { path: "/sales", label: "Sales", icon: BarChart3 },
  ];

  // Helper to determine if a navigation item is currently active
  const isActive = (path: string) => pathname?.startsWith(path);

  return (
    <header className="sticky top-0 z-10 border-b border-gray-200 bg-white/80 backdrop-blur-sm">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="flex h-16 items-center justify-between">
          {/* Logo and title */}
          <Link href="/" className="flex items-center gap-2">
            <div className="flex h-9 w-9 items-center justify-center rounded-lg bg-gradient-to-br from-indigo-500 to-purple-600 text-white shadow-sm">
              <CalendarDays className="h-5 w-5" />
            </div>
            <span className="text-xl font-bold text-gray-800">
              Event Manager
            </span>
          </Link>

          {/* Navigation */}
          <nav className="flex items-center gap-2">
            {navItems.map((item) => {
              const active = isActive(item.path);
              const Icon = item.icon;

              return (
                <Link
                  key={item.path}
                  href={item.path}
                  className={navLinkStyles({ active })}
                >
                  <Icon className={iconStyles({ active })} />
                  <span>{item.label}</span>
                </Link>
              );
            })}
          </nav>
        </div>
      </div>
    </header>
  );
}
