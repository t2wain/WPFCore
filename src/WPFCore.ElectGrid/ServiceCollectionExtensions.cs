using Microsoft.Extensions.DependencyInjection;
using WPFCore.ElectGrid.DG;
using WPFCore.ElectGrid.LV;
using WPFCore.ElectGrid.RPT;
using WPFCore.ElectGrid.TC;

namespace WPFCore.ElectGrid
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElectGrid(this IServiceCollection service)
        {
            service.AddTransient<LViewVM>();
            service.AddTransient<DGridVM>();
            return service;
        }
    }
}
