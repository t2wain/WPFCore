using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPFCore.Data.TV
{
    public interface IEquipRepo : IDisposable
    {
        Task<IEnumerable<EquipItem>> GetMotor();
        Task<IEnumerable<EquipItem>> GetOEE();
        Task<IEnumerable<EquipItem>> GetGenerators();
        Task<IEnumerable<EquipItem>> GetTransformers();
        Task<IEnumerable<EquipItem>> GetVFDs();
        Task<IEnumerable<EquipItem>> GetPDBs();
        Task<IEnumerable<EquipItem>> GetEquipment(int equipSubClass);
    }
}
