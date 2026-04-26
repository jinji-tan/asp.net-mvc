# Todo MVC with Authentication

The app handles full user authentication and allows users to manage their own private Todo lists.

## Core Features
*   **Secure Auth**: Custom registration and login using cookie-based authentication.
*   **Private Todos**: Every todo item is linked to a user ID, ensuring you only see your own tasks.
*   **Fast Data Access**: Built with raw SQL and Dapper.

## Getting Started

### 1. Database
You'll need SQL Server running. Execute the script in `mvc/SQL/MyApp.sql` to create the database, schema, and necessary tables (Users & TodoItems).

### 2. Configuration
Open `mvc/appsettings.json` and update the connection string to match your local setup:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MyAppDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

<!-- ### 3. Styling (Tailwind)
The project uses Tailwind's new CLI for styling. To watch for changes:
```bash
npm install tailwindcss @tailwindcss/cli
npx @tailwindcss/cli -i ./mvc/wwwroot/css/site.css -o ./mvc/wwwroot/css/output.css --watch
``` -->

### 3. Running the app
Just run the project from the `mvc` folder:
```bash
dotnet watch
```

## Project Layout
*   `Controllers/`: Handles the routing for Auth and Todo actions.
*   `Helpers/`: Contains the Dapper logic (`AuthHelper` and `TodoHelper`).
*   `DTOs/`: Simple objects for form validation and data transfer.
*   `Views/`: Razor templates for the UI.
*   `SQL/`: All database initialization scripts.