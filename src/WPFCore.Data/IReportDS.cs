using System.Data;
using System.Threading.Tasks;
using WPFCore.Data.Report;

namespace WPFCore.Data
{
    public interface IReportDS
    {
        Task<DataView> GetReportData(ReportDefinition def);
        Task<DataView> GetMotors();
        Task<DataView> GetOtherElectricalEquipment();
        Task<DataView> GetTransformers();
    }
}
