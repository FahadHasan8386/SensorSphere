
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using sensorSphere;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using sensorSphere.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register services
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<DeviceStateService>();
builder.Services.AddScoped<SensorStateService>();
builder.Services.AddScoped<AlertStateService>();

builder.Services.AddAuthorizationCore(); // Required for [Authorize]

await builder.Build().RunAsync();