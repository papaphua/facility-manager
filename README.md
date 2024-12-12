# Facility Manager

## How to launch
1. Run the following command to create migration within src/FacilityManager.Persistence:
```shell
dotnet ef migrations add Init --startup-project "../FacilityManager.App"
```
2. Run the following command to apply migration within src/FacilityManager.Persistence:
```shell
dotnet ef database update --startup-project "../FacilityManager.App"
```
3. Create a .env file within the FacilityManager.App directory and add the following content. Replace "value" with the API key you intend to use:
```env
API_KEY=value
```
4. To test the endpoints using Swagger, click the Authorize button at the top-right corner and enter the API key from your .env file.

## **Pagination**
- The PagingQuery record defines the parameters for pagination:
  - **PageNumber**:
    - Specifies which page of the dataset to retrieve.
  - **PageSize**:
    - Defines the number of items per page.
- **Behaviour**:
  -  If PagingQuery is not set, the API will return all data and treat it as a single page. The response will include paging information, considering the entire dataset as one page.
 
## Structure
- src
  - **FacilityManager.App** — the entry point of the application.
  - **FacilityManager.Application** — responsible for application logic.
  - **FacilityManager.Domain** — core business logic and rules.
  - **FacilityManager.Persistence** — responsible for managing data storage and retrieval.
  - **FacilityManager.Presentation** — handles user interaction and API endpoints.
- tests
  - **FacilityManager.Tests.Application** — unit tests for the Application layer.
 
## Patterns
1. **Generic Repository** — provides a standard way to handle data access for a specific entity type.
2. **Unit of Work** — provides methods to manage transactions and persist changes to the database.
3. **Result Pattern** — encapsulates the outcome of an operation, provides predefined errors for specific entity type.

## Third-Party Libraies
1. **Scrutor** — dependency injection enhancements allowing for automatic registration of services based on rules.
2. **Serilog.AspNetCore** — logging.
3. **Swashbuckle.AspNetCore** — documented API for testing purposes.
4. **AutoMapper** — mapping entities to DTOs.
5. **DotNetEnv** — loads envs from .env file.
