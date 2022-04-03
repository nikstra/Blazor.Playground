using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DataBinding.Pages.Components;
using DataBinding.Models;
using DataBinding.Pages.Components.Calendar;
using DataBinding.Services;
using DataBinding.Interfaces;
using DataBinding.Common;
using System.Text.Json.Serialization;
using DataBinding.Clients;
using DataBinding.Extensions;
using Microsoft.AspNetCore.Components.Web;

namespace DataBinding
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
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
            builder.Services.AddLocalization(options=>
            {
                options.ResourcesPath = "Shared.ResourceFiles";
            });

            var host = builder.Build();
            
            await host.SetDefaultCulture();

            await host.RunAsync();
        }
    }
}
