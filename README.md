# Congestion Tax Calculator

This project is a backend application focusing on implementing a Congestion Tax Calculator. The project adheres to Clean Architecture principles with support from Domain-Driven Design (DDD) concepts. It is structured into four main layers: API, Service, Infrastructure, and Core.

## Project Structure

- **API:** The presentation layer responsible for handling HTTP requests and responses. It has references to all other layers.
- **Service:** The application layer where tax calculations are performed. It has references to the Core layer.
- **Infrastructure:** The external access layer responsible for interacting with external systems. It has references to the Core layer.
- **Core:** The heart of the application containing the business logic for the domains. It is independent of other layers.

## Testing

Unit tests have been implemented for the Core and Service layers to ensure the correctness and reliability of the core business logic and application layer.

## Time Constraints and Future Improvements

Due to time constraints, the project is a minimal viable product. Given more time, the following improvements would be considered:

1. **Integration Tests:** Write integration tests for the Infrastructure layer to ensure end-to-end functionality.

2. **Authentication and Authorization:** Implement authentication features and define authorization policies to control user access to APIs.

3. **Advanced Specification and Repository Patterns:** Enhance the usage of a more sophisticated Specification Pattern and Repository Pattern for data querying and manipulation.

4. **Localization:** Implement localization for error messages instead of hard-coding them, providing a better user experience.

## Configuration

### Environment Variables

The following environment variables are required for the proper functioning of the application. Make sure to set these variables in your development environment.

1. **`CongestionTaxCalculator_ConnectionString`:**
   - Description: The connection string for the application's database.
   - Example Value: `Server=.;Database=CongestionTaxCalculator;Trusted_Connection=True;TrustServerCertificate=True;`

2. **`CongestionTaxCalculator_JwtSecret`:**
   - Description: The JWT secret for the application's JWT Bearer token authentication.
   - Example Value: `qwerty`

3. **`CongestionTaxCalculator_SuperadminPassword`:**
   - Description: The password for the SuperAdmin user which is inserted to the database as seed data during application initialization.
   - Example Value: `@Superadmin123`

## Database Migration

Before running the project for the first time, ensure that the database is created by applying the migrations using the Entity Framework Core tools. Open a terminal or command prompt and navigate to the project directory, then run the following command:

```bash
dotnet ef database update
```
Or, if you are using the Package Manager Console in Visual Studio, run:

```bash
update-database
```