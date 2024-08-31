using BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient for general use
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Configure HttpClient for ProductService
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://blazorwebassembly20240831193600.azurewebsites.net/")
});

// Register Radzen's NotificationService
builder.Services.AddScoped<NotificationService>();

// Configure Azure AD authentication
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    // Configure additional authentication options if needed
});

await builder.Build().RunAsync();
