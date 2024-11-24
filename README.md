# Field Permissions

This project is designed to manage field-based permissions. It is built with ASP.NET Core and Dapper, with LINQ-like query support provided by the `MicroOrm.Dapper.Repositories` library.

## Setup Instructions

### 1. Configure the Database Connection
Open the **appsettings.json** file located in the root of the `App.FieldPermissions` project and update the `ConnectionStrings:Default` section with your MSSQL connection details. Example:

```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
  }
}
```

### 2. Run Migrations
To set up the database schema, follow these steps:

#### Using the CLI
Run the following command in the project root directory:

```bash
dotnet ef database update --project App.FieldPermissions
```

#### Using PowerShell
Alternatively, navigate to the `App.FieldPermissions` project directory and run:

```powershell
Update-Database
```

### 3. Run the Project
Start the project and test the necessary APIs or field permissions. You can use an API client (such as Postman or curl) to test the endpoints.

### 4. Technologies Used
- **ASP.NET Core**: For Web API development.
- **Dapper**: A micro ORM.
- **MicroOrm.Dapper.Repositories**: Provides LINQ-like queries for Dapper.