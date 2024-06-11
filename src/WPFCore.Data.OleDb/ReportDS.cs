using ADOLib;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace WPFCore.Data.OleDb
{
    public class ReportDS : IReportDS
    {
        private readonly IServiceProvider _provider;

        public ReportDS(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public Task<DataView> GetMotors()
        {
            throw new NotImplementedException();
        }

        public Task<DataView> GetOtherElectricalEquipment()
        {
            throw new NotImplementedException();
        }

        public Task<DataView> GetTransformers()
        {
            throw new NotImplementedException();
        }

        public IDatabase NewDB() => 
            _provider.GetRequiredService<IDatabase>();

    }
}
