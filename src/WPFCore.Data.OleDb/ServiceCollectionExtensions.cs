using Microsoft.Extensions.DependencyInjection;
using WPFCore.Data.OleDb.TV;
using D = WPFCore.Data.TV;

namespace WPFCore.Data.OleDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOleDbData(this IServiceCollection service, string connString)
        {
            service.AddScoped<IDBFactory, DBFactory>(p => new DBFactory(connString));
            service.AddScoped<D.IEquipRepo, EquipRepo>();
            service.AddScoped<IReportDS, ReportDS>();
            return service;
        }
    }
}
