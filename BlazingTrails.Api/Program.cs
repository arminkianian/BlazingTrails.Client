var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging(); //This middleware enables the debugging of Blazor WebAssembly code
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles(); //This middleware enables the API to serve the Blazor application.
app.UseStaticFiles(); //This middleware enables static files to be served by the API.

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html"); //If a request doesn’t match to a controller, serve the index.html file from the Blazor project.

app.Run();
