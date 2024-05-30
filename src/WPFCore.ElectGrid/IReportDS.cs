using System.Data;

namespace WPFCore.ElectGrid
{
    public interface IReportDS
    {
        Task<DataView> GetMotors();
        Task<DataView> GetOtherElectricalEquipment();
        Task<DataView> GetTransformers();
    }
}
