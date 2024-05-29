using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFCore.Data.TV;

namespace WPFCore.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleData(this IServiceCollection service)
        {
            service.AddTransient<IEquipRepo, EquipRepo>();
            return service;
        }
    }
}
