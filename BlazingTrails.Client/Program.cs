using BlazingTrails.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//Just like other ASP.NET Core applications,
//Blazor apps start off as .NET console apps.
//What makes them a Blazor application is the type of host they run.
//In the case of Blazor WebAssembly, it runs a WebAssemblyHost.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Defines the root components for the application
builder.RootComponents.Add<App>("#app"); //the entry point to the application
builder.RootComponents.Add<HeadOutlet>("head::after"); //enables us to make modifications to the head element in the host page

// Configure and register services with the IServiceCollection
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Build and run an instance of WebAssemblyHost
// using the configuration defined with the WebAssemblyHostBuilder
await builder.Build().RunAsync();
