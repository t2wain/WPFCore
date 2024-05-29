using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFCore.ElectIndex.LB;
using WPFCore.ElectIndex.TV;

namespace WPFCore.ElectIndex
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElectIndex(this IServiceCollection service)
        {
            service.AddScoped<TVRepo>();
            service.AddTransient<TreeVM>();
            service.AddTransient<LBoxVM>();
            service.AddTransient<UTreeLBoxVM>();
            return service;
        }
    }
}
