using ADOLib;
using Microsoft.Extensions.DependencyInjection;
using WPFCore.Data.OleDb.TV;
using D = WPFCore.Data.TV;

namespace WPFCore.Data.OleDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOleDbData(this IServiceCollection service, string connString)
        {
            service.AddTransient<IDatabase, DataDB>(p => new DataDB(connString));
            service.AddTransient<D.IEquipRepo, EquipRepo>();
            service.AddTransient<IReportDS, ReportDS>();
            return service;
        }
    }
}
