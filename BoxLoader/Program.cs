using BoxLoader.Host.Profile;
using BoxLoader.Repository.Boxes;
using BoxLoader.Repository.Database;
using BoxLoader.Service.Boxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Setup dependency injection
        var serviceProvider = new ServiceCollection()
            .AddDbContext<IBoxLoaderDbContext, BoxLoaderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
            .AddScoped<IBoxService, BoxService>()
            .AddScoped<IBoxRepository, BoxRepository>()
            .AddAutoMapper(Assembly.GetAssembly(typeof(BoxProfile)))
            .BuildServiceProvider();

        // Example usage
        using (var scope = serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider.GetRequiredService<IBoxService>();
            // Use your service here
        }
    }
}