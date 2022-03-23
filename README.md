# Contacts
take-home job interview assignment
Uses [FastEndPoint](https://github.com/dj-nitehawk/FastEndpoints) and Entity Framework Core.

To run tests:
 * Create a user `contacts` and a database `contacts-test` on a Postgresql server

To run the API:
 * Create a user `contacts` and a database `contacts` on a Postgresql server
 * Run the EF Core migration (`dotnet ef database update` in a terminal or `update-database` in Package Manager Console in Visual Studio)
 * Start the webserver either using Visual Studio or `dotnet run` in a terminal.
 * Swagger docs available at <https://localhost:5142/swagger>
