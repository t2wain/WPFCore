using Microsoft.Extensions.DependencyInjection;
using WPFCore.ElectGrid.LV;

namespace WPFCore.ElectGrid
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElectGrid(this IServiceCollection service)
        {
            service.AddTransient<LViewVM>();
            return service;
        }
    }
}
