using BlazorMobile.Common;
using BlazorMobile.Common.Services;
using BlazorMobileAuth.Blazor.Helpers;
using BlazorMobileAuth.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMobileAuth.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });




            #region Services registration

            ServicesHelper.ConfigureCommonServices(builder.Services);


            builder.Services
               .AddScoped<IAuthenticationService, AuthenticationService>()
               .AddScoped<IUserService, UserService>()
               .AddScoped<IHttpService, HttpService>()
               .AddScoped<ILocalStorageService, LocalStorageService>();

            // configure http client
            builder.Services.AddScoped(x =>
            {
                var apiUrl = new Uri(builder.Configuration["apiUrl"]);

                // use fake backend if "fakeBackend" is "true" in appsettings.json
                if (builder.Configuration["fakeBackend"] == "true")
                    return new HttpClient(new FakeBackendHandler()) { BaseAddress = apiUrl };

                return new HttpClient() { BaseAddress = apiUrl };
            });

            var host = builder.Build();

            //var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            //await authenticationService.Initialize();


            #endregion


            BlazorMobileService.OnBlazorMobileLoaded += (object source, BlazorMobileOnFinishEventArgs eventArgs) =>
            {
                Console.WriteLine($"Initialization success: {eventArgs.Success}");
                Console.WriteLine("Device is: " + BlazorDevice.RuntimePlatform);
            };

            builder.RootComponents.Add<MobileApp>("app");

            await builder.Build().RunAsync();
        }
    }
}
