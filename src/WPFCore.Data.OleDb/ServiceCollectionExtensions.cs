using Microsoft.Extensions.DependencyInjection;
using WPFCore.Common.Data;
using WPFCore.Data.OleDb.TV;

namespace WPFCore.Data.OleDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOleDbData(this IServiceCollection service, 
            string connString, string reportFolder)
        {
            service.AddScoped<IDBFactory, DBFactory>(p => new DBFactory(connString));
            service.AddScoped<IEquipRepo, EquipRepoDB>();
            service.AddScoped<IReportDS, ReportDS>(p =>
            {
                var df = p.GetRequiredService<IDBFactory>();
                return new ReportDS(df, reportFolder);
            });
            return service;
        }
    }
}
