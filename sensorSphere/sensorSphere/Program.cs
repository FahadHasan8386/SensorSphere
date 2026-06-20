
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using sensorSphere;
using sensorSphere.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<DeviceStateService>();
builder.Services.AddScoped<SensorStateService>();
builder.Services.AddScoped<AlertStateService>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();