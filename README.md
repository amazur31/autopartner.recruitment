# autopartner.recruitment


## Local Setup

### Requirements

- PostgreSQL 15.4 or higher [Download PostgreSQL](https://www.postgresql.org/download/)
- .NET 6 [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Steps
1. Connect to the PostgreSQL database.

2. Create a database named `autopartner`.

3. Run the following command in Visual Studio to initialize user secrets for the project:

`dotnet user-secrets init --project "src\Autopartner.Task.App"`

4. Add the database connection secret:

`dotnet user-secrets --project "src\Autopartner.Task.App" set "ConnectionStrings:DefaultConnection" "User ID=<USERNAME>;Password=<PASSWORD>;Host=localhost;Port=5432;Database=autopartner;"`

5. Run the database migration using the following command:

`update-database`