# Event Ticketing System Frontend

A responsive, modern web application that integrates with the Event Ticketing API to display events and sales analytics.

## Features

- Browse upcoming events with the ability to filter by time period (30, 60, or 180 days)
- Sort events by name or date in the events table
- View detailed information about individual events, including ticket sales
- Analyze top selling events by ticket count and revenue
- Error handling for API request failures

## Getting Started

### Prerequisites

- Node.js 18.x or higher
- npm or yarn
- Event Ticketing API running locally or accessible via network (Please read the README.MD file in EventTicketingSysem directory for server launch)

### Installation

1. Clone this repository 

2. Running the Application

```bash
cd to root folder
```

3. Install dependencies

```bash
npm install
```

4. Configure the connection to the API

By default, the application connects to the API at `http://localhost:5134/api`. If your API is running on a different URL, you can specify this using an environment variable:

```bash
# Create a .env.local file with the following content and also change content in lib/api.ts file to accomodate for the new base url:
NEXT_PUBLIC_API_BASE_URL=http://your-api-url
```

### Development

Run the development server:

```bash
npm run dev
```

This will start the application on [http://localhost:3000](http://localhost:3000).

### Building for Production

To create a production build:

```bash
npm run build
```

To start the production build:

```bash
npm run start
```

## Project Structure

```
src/
├── app/                  # Next.js app directory
│   ├── events/           # Events pages and components
│   │   ├── [id]/         # Event details page
│   │   ├── _components/  # Event-related components
│   │   ├── _hooks/       # Custom hooks for events
│   │   └── page.tsx      # Events listing page
│   ├── sales/            # Sales pages and components
│   │   ├── _components/  # Sales-related components
│   │   ├── _hooks/       # Custom hooks for sales data
│   │   └── page.tsx      # Sales summary page
│   ├── globals.css       # Global styles
│   ├── layout.tsx        # Root layout
│   └── page.tsx          # Home page
├── components/           # Shared components
├── lib/                  # Utilities and API client
├── types/                # TypeScript definitions
└── utils/                # Utility functions
```

## API Integration

The frontend interacts with the Event Ticketing API using the following endpoints:

### Events Endpoints

- `GET /api/events?days={30|60|180}` - Get upcoming events for the specified time period
- `GET /api/events/{id}` - Get details for a specific event

### Tickets Endpoint

- `GET /api/tickets/event/{eventId}` - Get all tickets sold for a specific event

### Sales Analytics Endpoints

- `GET /api/sales/top-by-count?count={number}` - Get top events by number of tickets sold
- `GET /api/sales/top-by-revenue?count={number}` - Get top events by total revenue

The API integration is handled by the API client in `src/lib/api.ts`, which uses axios for HTTP requests.

## Technologies Used

- **React** - UI library
- **Next.js** - React framework for server-rendered applications
- **TypeScript** - Type safety and improved developer experience
- **Tailwind CSS** - Utility-first CSS framework for styling
- **Axios** - HTTP client for API requests
- **date-fns** - Date manipulation library
- **React Icons** - Icon library
