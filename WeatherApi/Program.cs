using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add support for controllers
builder.Services.AddHttpClient();  // Register HttpClient for dependency injection

// Add CORS policy to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow requests from any origin
              .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any HTTP headers
    });
});

// Add Swagger for API documentation (optional)
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // Enable Swagger middleware
    app.UseSwaggerUI();     // Enable Swagger UI
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseRouting();           // Enable routing
app.UseAuthorization();     // Enable authorization (if needed)

app.MapControllers();       // Map controller routes

app.Run();                  // Run the application