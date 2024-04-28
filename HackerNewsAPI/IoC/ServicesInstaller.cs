
using HackerNewsAPI.Middlewares;
using HackerNewsAPI.Services.Abstraction;
using HackerNewsAPI.Services.Implementation;
using HackerNewsAPI.Settings;

namespace HackerNewsAPI.IoC;

public class ServicesInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<GlobalExceptionMiddleware>();
        services.AddScoped<IHackerNewsService, HackerNewsService>();

        services.AddHttpClient<IHackerNewsService, HackerNewsService>((serviceProvider, client) =>
        {
            HackerNewsSettings hackerNewsSettings = new();
            configuration.GetSection(nameof(HackerNewsSettings)).Bind(hackerNewsSettings);

            client.BaseAddress = new Uri(hackerNewsSettings.HackerNewsUrl);
        });

    }
}

/*
 Registration of a global exception handler, services with business logic, 
 and injection of Typed HttpClient (implementation of IHttpClientFactory) in a secure manner.
 Retrieving the baseAddress from appsettings has its pros and cons, 
 but since the link doesn't contain sensitive data, it's acceptable to keep the address
 in appsettings.
 */