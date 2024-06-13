using Microsoft.Extensions.DependencyInjection;
using WPFCore.Common.Data;
using WPFCore.Data.OleDb.TV;
using WPFCore.Data.TV;

namespace WPFCore.Data.OleDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOleDbData(this IServiceCollection service, string connString)
        {
            service.AddScoped<IDBFactory, DBFactory>(p => new DBFactory(connString));
            service.AddScoped<IEquipRepo, EquipRepoDB>();
            service.AddScoped<IReportDS, ReportDS>();
            return service;
        }
    }
}
