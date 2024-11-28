using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUi.Clients;
using WebUi.Common;
using WebUi.Extensions;
using WebUi.Interfaces;
using WebUi.Services;
using WebUi;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new DateOnlyJsonConverter());
    options.Converters.Add(new JsonStringEnumConverter());
    options.PropertyNameCaseInsensitive = true;
    // options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<HolidaysClient>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IHolidaysService, HolidaysService>();
builder.Services.AddScoped<ICalendarProvider, CalendarProvider>();
builder.Services.AddLocalization(options=>
{
    options.ResourcesPath = "Shared.ResourceFiles";
});

var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();
