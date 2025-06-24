# Weather App - ASP.NET Core Web API + React Frontend

A modern weather application built with .NET 8 Web API backend and React TypeScript frontend that demonstrates clean architecture, dependency injection, and comprehensive unit testing.

## Features

### Backend (.NET 8 Web API)
- **Clean Architecture**: Separation of concerns with Core, Infrastructure, and API layers
- **Dependency Injection**: All services and HTTP clients are registered in the DI container
- **Weather API Integration**: Real-time weather data from OpenWeatherMap API
- **Temperature Conversion**: Fahrenheit to Celsius conversion in service layer
- **Comprehensive Error Handling**: Proper HTTP status codes and error responses
- **CORS Support**: Configured for React frontend integration
- **Swagger Documentation**: API documentation and testing interface

### Frontend (React TypeScript)
- **Modern React**: Built with React 18 and TypeScript
- **Responsive Design**: Mobile-friendly UI with CSS Grid and Flexbox
- **Real-time Weather Data**: Live weather information display
- **Error Handling**: User-friendly error messages and loading states
- **Country → City → Weather Flow**: Intuitive step-by-step user experience

### Testing
- **Unit Tests**: Comprehensive test coverage for services and controllers
- **Mocked Dependencies**: All external calls are mocked for offline testing
- **Temperature Conversion Tests**: Verification of Fahrenheit to Celsius conversion logic
- **Controller Tests**: Success and error flow testing with mocked services

## Technology Stack

### Backend
- **.NET 8**: Latest LTS version
- **ASP.NET Core Web API**: RESTful API framework
- **System.Text.Json**: JSON serialization
- **Microsoft.Extensions.Http**: HTTP client factory
- **xUnit**: Unit testing framework
- **Moq**: Mocking framework for unit tests

### Frontend
- **React 18**: Modern React with hooks
- **TypeScript**: Type safety and better development experience
- **Axios**: HTTP client for API calls
- **CSS3**: Modern styling with gradients and backdrop filters

## Project Structure

```
WeatherApp/
├── WeatherApp.Api/              # Web API layer
│   ├── Controllers/             # API controllers
│   ├── Program.cs              # Application startup and DI configuration
│   └── appsettings.json        # Configuration files
├── WeatherApp.Core/            # Domain layer
│   ├── Interfaces/             # Service interfaces
│   └── Models/                 # Domain models and DTOs
├── WeatherApp.Infrastructure/   # Infrastructure layer
│   └── Services/               # Service implementations
├── WeatherApp.Tests/           # Unit tests
│   ├── WeatherServiceTests.cs  # Weather service tests
│   ├── CountryServiceTests.cs  # Country service tests
│   └── ControllersTests.cs     # Controller tests
└── weather-app-frontend/       # React frontend
    ├── public/                 # Static files
    └── src/                    # React components and logic
```

## API Endpoints

### Countries
- `GET /api/countries` - Get all available countries
- `GET /api/countries/{countryCode}/cities` - Get cities for a specific country

### Weather
- `GET /api/weather/{cityName}` - Get current weather for a city

## Weather Data Response

The weather endpoint returns comprehensive weather information:

```json
{
    "city": "New York",
    "country": "US",
    "timeUtc": "2025-01-24T15:30:00Z",
    "wind": {
        "speedMph": 12.5,
        "direction": "NW"
    },
    "visibilityMiles": 6.2,
    "skyConditions": "clear sky",
    "temperatureFahrenheit": 68.0,
    "temperatureCelsius": 20.0,
    "dewPointFahrenheit": 45.0,
    "dewPointCelsius": 7.22,
    "relativeHumidity": 65,
    "pressureInHg": 29.92
}
```

## Setup Instructions

### Prerequisites
- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** - [Download here](https://nodejs.org/)
- **OpenWeatherMap API Key** - [Register here](https://openweathermap.org/api)

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd WeatherApp
   ```

2. **Configure API Key**
   Update `appsettings.json` and `appsettings.Development.json`:
   ```json
   {
     "OpenWeatherMap": {
       "ApiKey": "YOUR_ACTUAL_API_KEY_HERE"
     }
   }
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run tests**
   ```bash
   dotnet test
   ```

6. **Start the API server**
   ```bash
   cd WeatherApp.Api
   dotnet run
   ```
   
   The API will be available at: `http://localhost:5000`
   Swagger UI: `http://localhost:5000/swagger`

### Frontend Setup

1. **Navigate to frontend directory**
   ```bash
   cd weather-app-frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Start the development server**
   ```bash
   npm start
   ```
   
   The frontend will be available at: `http://localhost:3000`

## Usage

1. **Start both servers** (API and React frontend)
2. **Open browser** to `http://localhost:3000`
3. **Select a country** from the dropdown
4. **Select a city** from the populated city dropdown
5. **View weather data** displayed automatically

## Configuration

### Environment Variables

The application uses configuration files for settings:

- **Development**: `appsettings.Development.json`
- **Production**: `appsettings.json`

### CORS Configuration

The API is configured to allow requests from:
- `http://localhost:3000` (React development server)
- `https://localhost:3000` (React development server with HTTPS)

## Testing

### Running All Tests
```bash
cd WeatherApp
dotnet test
```

### Test Coverage
- **WeatherService**: Temperature conversion, HTTP success/failure scenarios
- **CountryService**: Country and city data retrieval
- **Controllers**: API endpoints with mocked dependencies

### Test Categories
- **Unit Tests**: Service logic and business rules
- **Integration Tests**: Controller endpoints with dependency injection
- **Offline Tests**: All external API calls are mocked

## Design Decisions

### Architecture Patterns
- **Clean Architecture**: Clear separation of concerns
- **Dependency Injection**: Loose coupling and testability
- **Repository Pattern**: Data access abstraction (in-memory for demo)
- **Service Layer**: Business logic encapsulation

### Error Handling
- **HTTP Status Codes**: Proper REST API status codes
- **User-Friendly Messages**: Clear error messages for end users
- **Logging**: Structured logging for debugging

### Performance Considerations
- **HTTP Client Factory**: Efficient HTTP client management
- **Async/Await**: Non-blocking operations
- **Response Caching**: Browser caching for static country/city data

## Assumptions

1. **In-Memory Data**: Countries and cities are seeded in memory (not from database)
2. **API Key Security**: API key is stored in configuration files (use Azure Key Vault for production)
3. **Error Handling**: Basic error handling implemented (can be extended with retry policies)
