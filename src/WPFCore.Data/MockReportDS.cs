using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WPFCore.Common.Data;
using WPFCore.Data.Report;

namespace WPFCore.Data
{
    public class MockReportDS : IReportDS
    {
        public void ClearConnectionPool() => throw new NotImplementedException();

        public Task<DataView> GetReportData(ReportDefinition def) => throw new NotImplementedException();

        public Task<ReportDefinition> GetReportDefinition(string reportId) => throw new NotImplementedException();

        public Task<ReportDefinition[]>? GetReportDefinitions() => Task.FromResult<ReportDefinition[]>([]);

        public Task<List<ColumnDefinition>> GetUpdatedColumnDefinitions(ReportDefinition def) => throw new NotImplementedException();

        public Task SaveReportDefinition(ReportDefinition def) => throw new NotImplementedException();
    }
}
