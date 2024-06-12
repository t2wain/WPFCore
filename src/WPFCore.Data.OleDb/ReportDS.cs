using System.Data;
using WPFCore.Data.Report;

namespace WPFCore.Data.OleDb
{
    internal class ReportDS : IReportDS
    {
        private readonly IDBFactory _dbfact;

        public ReportDS(IDBFactory dbfact)
        {
            this._dbfact = dbfact;
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

        public Task<DataView> GetReportData(ReportDefinition def)
        {
            var db = _dbfact.NewDB();
            return ReportUtil.LoadReport(db, def).ContinueWith(t => 
            {
                db.Dispose();
                return t.Result;
            });
        }

        public Task<List<ColumnDefinition>> GetUpdatedColumnDefinitions(ReportDefinition def)
        {
            var db = _dbfact.NewDB();
            return Task.Factory.StartNew(() => ReportUtil.GetUpdatedColumnDefinitions(db, def))
                .ContinueWith(t => 
                { 
                    db.Dispose();
                    return t.Result;
                });
        }

    }
}
