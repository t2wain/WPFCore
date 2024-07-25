using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFCore.Data.OleDb;
using WPFCore.ElectIndex;
using WPFCore.ElectGrid;

namespace WPFCore.App
{
    public static class ServiceCollectionExtensions
    {
        static IHost _host = null!;
        public static IHost GetHost()
        {
            if (_host == null)
            {
                var builder = new HostApplicationBuilder();
                builder.Services.ConfigureServices(builder.Configuration);
                _host = builder.Build();
            }
            return _host;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection service, IConfiguration _config)
        {
            service.AddOleDbData(_config.GetConnectionString("Default")!, _config["ReportFolder"]!);
            service.AddElectIndex();
            service.AddElectGrid(); 
            return service;
        }
    }
}
