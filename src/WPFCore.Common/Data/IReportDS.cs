using System.Data;
using WPFCore.Data.Report;

namespace WPFCore.Common.Data
{
    public interface IReportDS
    {
        Task<DataView> GetReportData(ReportDefinition def);
        Task<List<ColumnDefinition>> GetUpdatedColumnDefinitions(ReportDefinition def);
        Task<DataView> GetMotors();
        Task<DataView> GetOtherElectricalEquipment();
        Task<DataView> GetTransformers();
    }
}
