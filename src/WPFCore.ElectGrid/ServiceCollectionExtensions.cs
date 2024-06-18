using Microsoft.Extensions.DependencyInjection;
using WPFCore.ElectGrid.LV;
using WPFCore.ElectGrid.TC;

namespace WPFCore.ElectGrid
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElectGrid(this IServiceCollection service)
        {
            service.AddTransient<LViewVM>();
            service.AddTransient<UTabConrolVM>();
            return service;
        }
    }
}
