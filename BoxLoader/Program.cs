using BoxLoader.Host.Profile;
using BoxLoader.Repository.Boxes;
using BoxLoader.Repository.Database;
using BoxLoader.Service.Boxes;
using BoxLoader.Service.Files;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to monitor as an argument");
            return;
        }

        var serviceProvider = new ServiceCollection()
            .AddDbContext<IBoxLoaderDbContext, BoxLoaderDbContext>()
            .AddScoped<IBoxService, BoxService>()
            .AddScoped<IBoxRepository, BoxRepository>()
            .AddScoped<IFileService, FileService>()
            .AddAutoMapper(Assembly.GetAssembly(typeof(BoxProfile)))
            .BuildServiceProvider();


        using (var scope = serviceProvider.CreateScope())
        {
            var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

            fileService.MonitorDirectory(args[0]);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}