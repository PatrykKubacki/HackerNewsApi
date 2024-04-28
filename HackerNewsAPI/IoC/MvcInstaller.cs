using Microsoft.OpenApi.Models;

namespace HackerNewsAPI.IoC;
public class MvcInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x => {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Hacker News API", Version = "v1" });
        }); ;
    }
}

/*
 Registration of services related to ASP.NET logic and Swagger.
 */