using BlazingTrails.Client;
using BlazingTrails.Client.Features.Auth;
using BlazingTrails.Client.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Claims;

//Just like other ASP.NET Core applications,
//Blazor apps start off as .NET console apps.
//What makes them a Blazor application is the type of host they run.
//In the case of Blazor WebAssembly, it runs a WebAssemblyHost.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Defines the root components for the application
builder.RootComponents.Add<App>("#app"); //the entry point to the application
builder.RootComponents.Add<HeadOutlet>("head::after"); //enables us to make modifications to the head element in the host page

// Configure and register services with the IServiceCollection
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services
    .AddHttpClient("SecureAPIClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddOidcAuthentication(options =>
    {
        builder.Configuration.Bind("Auth0", options.ProviderOptions);
        options.ProviderOptions.ResponseType = "code";

        //By default, Auth0 returns the user’s email address as the value for the name claim.
        //Blazor, by default, maps the name claim to the Name property on the User.Identity object
        //options.UserOptions.NameClaim = ClaimTypes.Name;
    }).AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

builder.Services.AddScoped<AppState>();

// Build and run an instance of WebAssemblyHost
// using the configuration defined with the WebAssemblyHostBuilder
await builder.Build().RunAsync();
