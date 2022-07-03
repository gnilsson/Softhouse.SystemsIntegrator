
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Softhouse.Converter;
using Softhouse.Parser;
using Softhouse.Server.Services;
using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("Softhouse.Server.Tests")]
namespace Softhouse.Server;

public class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.Services.GetService<IServerService>()!.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory());
            })
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IServerService, ServerService>();
                services.AddTransient<IXmlConvertingService, XmlConvertingService>();
                services.AddTransient<IFormatParsingService, FormatParsingService>();
                services.AddTransient<IConsoleManager, ConsoleManager>();
                services.AddTransient<IPersonBuilderService, PersonBuilderService>();
            });

        return hostBuilder;
    }
}
