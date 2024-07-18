using System.Data;
using WPFCore.Data.Report;

namespace WPFCore.Common.Data
{
    public interface IReportDS
    {
        Task<ReportDefinition> GetReportDefinition(string reportId);
        Task<DataView> GetReportData(ReportDefinition def);
        Task<List<ColumnDefinition>> GetUpdatedColumnDefinitions(ReportDefinition def);
        Task SaveReportDefinition(ReportDefinition def);
    }
}
