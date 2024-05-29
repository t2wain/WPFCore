using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WPFCore.App
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
            _host = ServiceCollectionExtensions.GetHost();
        }
    }

}
