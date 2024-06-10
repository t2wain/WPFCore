using ADOLib;
using System.Data;

namespace WPFCore.ElectGrid
{
    public interface IReportDS
    {
        IDatabase DB { get; set; }
        Task<DataView> GetMotors();
        Task<DataView> GetOtherElectricalEquipment();
        Task<DataView> GetTransformers();
    }
}
