namespace HackerNewsAPI.IoC;
public interface IInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}

/*
 Interface for marking service installers.
 */