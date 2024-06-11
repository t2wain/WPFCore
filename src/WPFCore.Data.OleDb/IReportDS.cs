using ADOLib;
using System.Data;

namespace WPFCore.Data.OleDb
{
    public interface IReportDS
    {
        IDatabase NewDB();
        Task<DataView> GetMotors();
        Task<DataView> GetOtherElectricalEquipment();
        Task<DataView> GetTransformers();
    }
}
