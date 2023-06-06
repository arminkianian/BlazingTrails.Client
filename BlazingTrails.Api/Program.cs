using BlazingTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlazingTrailsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"))
);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("BlazingTrails.Shared")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging(); //This middleware enables the debugging of Blazor WebAssembly code
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles(); //This middleware enables the API to serve the Blazor application.
app.UseStaticFiles(); //This middleware enables static files to be served by the API.
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/Images")
});

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html"); //If a request doesn’t match to a controller, serve the index.html file from the Blazor project.

app.Run();
