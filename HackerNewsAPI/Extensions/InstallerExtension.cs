using HackerNewsAPI.IoC;

namespace HackerNewsAPI.Extensions;
public static class InstallerExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        installers.ForEach(service => service.Install(services, configuration));
    }
}

/*
 An extension method that collects implementations of the IInstaller interface from the 
 Assembly and registers services.
 */