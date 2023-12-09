# Congestion Tax Calculator

This project is a backend application focusing on implementing a Congestion Tax Calculator. The project adheres to Clean Architecture principles with support from Domain-Driven Design (DDD) concepts. It is structured into four main layers: API, Service, Infrastructure, and Core.

## Project Structure

- **API:** The presentation layer responsible for handling HTTP requests and responses. It has references to all other layers.
- **Service:** The application layer where tax calculations are performed. It has references to the Core layer.
- **Infrastructure:** The external access layer responsible for interacting with external systems. It has references to both Core and Service layers.
- **Core:** The heart of the application containing the business logic for the domains. It is independent of other layers.

## Layers Relationships

- **API:** References all layers (Core, Service, and Infrastructure).
- **Service:** References Core for business logic.
- **Infrastructure:** References Core for business logic and Service for application layer functionality.

## Testing

Unit tests have been implemented for the Core and Service layers to ensure the correctness and reliability of the core business logic and application layer.

## Time Constraints and Future Improvements

Due to time constraints, the project is a minimal viable product. Given more time, the following improvements would be considered:

1. **Integration Tests:** Write integration tests for the Infrastructure layer to ensure end-to-end functionality.

2. **Authentication and Authorization:** Implement authentication features and define authorization policies to control user access to APIs.

3. **Advanced Specification and Repository Patterns:** Enhance the usage of Specification and Repository patterns for more sophisticated data querying and manipulation.

4. **Localization:** Implement localization for error messages instead of hard-coding them, providing a better user experience.