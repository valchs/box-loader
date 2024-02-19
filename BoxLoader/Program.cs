using BoxLoader.Host.Profile;
using BoxLoader.Repository.Boxes;
using BoxLoader.Repository.Database;
using BoxLoader.Service.Boxes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<IBoxLoaderDbContext, BoxLoaderDbContext>()
            .AddScoped<IBoxService, BoxService>()
            .AddScoped<IBoxRepository, BoxRepository>()
            .AddAutoMapper(Assembly.GetAssembly(typeof(BoxProfile)))
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var boxService = scope.ServiceProvider.GetRequiredService<IBoxService>();

            var result = await boxService.Get("1");
        }
    }
}