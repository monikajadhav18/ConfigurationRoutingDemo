using ConfigurationRoutingDemo.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// 1. Register the AppSettings section with the Options pattern.
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

// 2. Build the application.
var app = builder.Build();

// --------------------------------------------------
// ENDPOINT 1: Read a setting using IConfiguration
// GET /api/info
// --------------------------------------------------

app.MapGet("/api/info", (IConfiguration configuration) =>
{
    string? applicationName =
        configuration["AppSettings:ApplicationName"];

    return Results.Ok(new
    {
        ApplicationName = applicationName
    });
});

// --------------------------------------------------
// ENDPOINT 2: Read settings using IOptions<T>
// GET /api/settings
// --------------------------------------------------

app.MapGet("/api/settings", (IOptions<AppSettings> options) =>
{
    AppSettings settings = options.Value;

    return Results.Ok(new
    {
        settings.ApplicationName,
        settings.MaxEmployees,
        settings.EnableReports
    });
});

// --------------------------------------------------
// ENDPOINT 3: Route parameter and integer constraint
// GET /api/employees/10
// --------------------------------------------------

app.MapGet("/api/employees/{id:int}", (int id) =>
{
    return Results.Ok(new
    {
        Message = "Employee retrieved successfully",
        EmployeeId = id
    });
});

// --------------------------------------------------
// ENDPOINT 4: Route group
// GET /api/v1/employees
// GET /api/v1/employees/10
// --------------------------------------------------

var employeeGroup = app.MapGroup("/api/v1/employees");

employeeGroup.MapGet("/", () =>
{
    return Results.Ok(new
    {
        Message = "All employees"
    });
});

employeeGroup.MapGet("/{id:int}", (int id) =>
{
    return Results.Ok(new
    {
        Message = "Employee retrieved from route group",
        EmployeeId = id
    });
});

// 3. Start the application.
app.Run();