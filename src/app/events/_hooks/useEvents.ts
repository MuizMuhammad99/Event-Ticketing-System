"use client";

import { useState, useEffect } from "react";
import { api } from "@/lib/api";
import { Event, DaysFilterType } from "@/types";

/**
 * Custom hook for fetching and managing upcoming events
 *
 * @param initialDays - Initial time period for fetching events (30, 60, or 180 days)
 * @returns State and functions for events management
 */
export function useEvents(initialDays: DaysFilterType = 30) {
  const [days, setDays] = useState<DaysFilterType>(initialDays);
  const [events, setEvents] = useState<Event[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetches events from API based on current days filter
   */
  const fetchEvents = async () => {
    setIsLoading(true);
    setError(null);

    try {
      const data = await api.events.getUpcoming(days);
      setEvents(data);
    } catch (err) {
      console.error("Failed to fetch events:", err);
      setError("Failed to load events. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  };

  // Refetch events when days filter changes
  useEffect(() => {
    fetchEvents();
  }, [days]);

  return {
    events,
    days,
    setDays,
    isLoading,
    error,
    refetch: fetchEvents,
  };
}

/**
 * Custom hook for fetching and managing single event details
 *
 * @param id - Event identifier to fetch
 * @returns State and functions for event details
 */
export function useEventDetails(id: string) {
  const [event, setEvent] = useState<Event | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetches specific event details from API
   */
  const fetchEventDetails = async () => {
    if (!id) return;

    setIsLoading(true);
    setError(null);

    try {
      const data = await api.events.getById(id);
      setEvent(data);
    } catch (err: any) {
      console.error("Failed to fetch event details:", err);
      const errorMsg =
        err.response?.status === 404
          ? "Event not found"
          : "Failed to load event details. Please try again later.";
      setError(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  // Fetch event details on mount or when ID changes
  useEffect(() => {
    fetchEventDetails();
  }, [id]);

  return {
    event,
    isLoading,
    error,
    refetch: fetchEventDetails,
  };
}
