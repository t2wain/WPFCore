using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFCore.Data;
using WPFCore.Data.OleDb;
using WPFCore.Data.TV;

namespace WPFCore.Test
{
    public class Context : IDisposable
    {
        IHost _host = null!;

        public Context()
        {
            var host = new HostApplicationBuilder();
            host.Services.AddOleDbData("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\\devgit\\Data\\SPEL.accdb");
            _host = host.Build();
        }

        public IEquipRepo Repo => 
            _host.Services.GetRequiredService<IEquipRepo>();

        public IReportDS ReportDS => 
            _host.Services.GetRequiredService<IReportDS>();

        public void Dispose()
        {
            Repo.Dispose();
        }
    }
}
