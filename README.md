**NZWalks API**

The NZWalks API is designed to manage walks across various regions in New Zealand. It offers endpoints for user authentication, region and walk management, as well as image uploads associated with walks.

**Key Technologies**

- **ASP.NET Core:** The API is built using the ASP.NET Core framework, which provides a robust and flexible platform for developing web applications and APIs.
- **Entity Framework Core:** This is utilized as the Object-Relational Mapping (ORM) tool, allowing smooth interaction with the database.
- **Microsoft Identity:** Handles user authentication and role-based authorization, ensuring secure access to the API.
- **AutoMapper:** Employed for efficient object-to-object mapping between domain models and Data Transfer Objects (DTOs).
- **Microsoft.AspNetCore.Mvc:** The MVC framework within ASP.NET Core is used to process HTTP requests and responses.
- **Microsoft.Extensions.Logging:** Logging within the API is managed via the ILogger interface provided by ASP.NET Core.
- **IWebHostEnvironment:** Used to access information about the web hosting environment, such as the content root path.
- **IHttpContextAccessor:** This is used to access the HttpContext, enabling the retrieval of the base URL for forming image file paths.
API Endpoints

**AuthController**

- **POST /api/Auth/Register:** Registers a new user with a username, email, and password. Roles can also be assigned during the registration process.
- **POST /api/Auth/Login:** Authenticates a user using their email and password, returning a JWT token for subsequent API requests.
RegionController

- **GET /api/Region:** Retrieves a list of all regions.
- **GET /api/Region/{id}:** Fetches details of a specific region using its ID.
- **POST /api/Region:** Creates a new region with the given data.
- **PUT /api/Region/{id}:** Updates an existing region with the provided information.
- **DELETE /api/Region/{id}:** Deletes a region based on its ID.
WalkController

- **GET /api/Walk:** Retrieves a list of walks with options for filtering and pagination.
- **GET /api/Walk/{id}:** Retrieves details of a specific walk using its ID.
- **POST /api/Walk:** Creates a new walk with the given details.
- **PUT /api/Walk/{id}:** Updates an existing walk with the provided data.
- **DELETE /api/Walk/{id}:** Deletes a walk based on its ID.
ImageController

- **POST /api/Image/Upload:** Uploads an image file for a walk. The image is validated based on its extension and file signature before being accepted.
