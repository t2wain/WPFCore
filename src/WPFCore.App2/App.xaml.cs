using Microsoft.Extensions.Hosting;
using System.Windows;
using WPFCore.ElectIndex;
using WPFCore.Data;

namespace WPFCore.App2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public IServiceProvider Provider => _host.Services;

        public App()
        {
            var builder = new HostApplicationBuilder();
            builder.Services.AddSampleData();
            builder.Services.AddElectIndex();
            _host = builder.Build();
        }

    }

}
