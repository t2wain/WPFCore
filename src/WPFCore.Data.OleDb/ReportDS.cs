using System.Data;
using WPFCore.Common.Data;
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

        #region Others

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

        #endregion

        public Task<ReportDefinition> GetReportDefinition(string reportId) =>
            ReportUtil.DeserializeReportDefinitionFromFile(reportId);

        public Task<DataView> GetReportData(ReportDefinition def)
        {
            var db = _dbfact.NewDB();
            var t = ReportUtil.LoadReportAsync(db, def);
            t.ContinueWith(_ => db.Dispose());
            return t;
        }

        public Task<List<ColumnDefinition>> GetUpdatedColumnDefinitions(ReportDefinition def)
        {
            using var db = _dbfact.NewDB();
            var t = ReportUtil.GetUpdatedColumnDefinitionsAsync(db, def);
            t.ContinueWith(_ => db.Dispose(), TaskContinuationOptions.ExecuteSynchronously);
            return t;
        }

        public Task SaveReportDefinition(ReportDefinition def) =>
            ReportUtil.SaveReportDefinition(def);
    }
    
}