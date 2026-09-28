using BlazorHybridDemo.Shared.Services;
using BlazorHybridDemo.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorHybridDemo.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add device-specific services used by the BlazorHybridDemo.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            await builder.Build().RunAsync();
        }
    }
}
