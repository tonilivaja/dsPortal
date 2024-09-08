using dsPortal.Client.Core.ExceptionHandling;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace dsPortal.Client.Core.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebAssemblyHostEnvironment hostEnvironment, WebAssemblyHostConfiguration configuration)
        {
            InitializeHttpClient(services, hostEnvironment);
            InitializeServices(services);

            InitializeMudServices(services);

            return services;
        }

        private static void InitializeHttpClient(IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
        {
            services.AddHttpClient("dsPortal.Web.ServerAPI", (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(hostEnvironment.BaseAddress);
                client.EnableIntercept(serviceProvider);
            })
            .AddHttpMessageHandler<HttpErrorHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("dsPortal.Web.ServerAPI"));

            services.AddHttpClientInterceptor();

            services.AddScoped<HttpErrorHandler>();
            services.AddSingleton<IHttpErrorService, HttpErrorService>();
        }

        private static void InitializeServices(IServiceCollection services)
        {
            //services.AddScoped<, >();
        }

        private static void InitializeMudServices(IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
        }
    }
}
