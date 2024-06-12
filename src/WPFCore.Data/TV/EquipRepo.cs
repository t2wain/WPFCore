using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPFCore.Data.TV
{
    public class EquipRepo : IEquipRepo
    {

        public Task<IEnumerable<EquipItem>> GetMotor() =>
            GetEquipment(35);

        public Task<IEnumerable<EquipItem>> GetOEE() =>
            GetEquipment(45);

        public Task<IEnumerable<EquipItem>> GetGenerators() =>
            GetEquipment(28);

        public Task<IEnumerable<EquipItem>> GetTransformers() =>
            GetEquipment(11);

        public Task<IEnumerable<EquipItem>> GetVFDs() =>
            GetEquipment(12);

        public Task<IEnumerable<EquipItem>> GetPDBs() =>
            GetEquipment(34);

        public virtual Task<IEnumerable<EquipItem>> GetEquipment(int equipSubClass)
        {
            string tag = equipSubClass switch
            {
                35 => "MTR",
                45 => "OEE",
                28 => "GEN",
                11 => "XFR",
                12 => "VDF",
                34 => "PDB",
                _ => "UNK"
            };

            var lst = Enumerable.Range(1, 10).Select(i => new EquipItem()
            {
                ID = $"{tag}-{i.ToString().PadLeft(3, '0')}",
                Name = $"{tag}-{i.ToString().PadLeft(3, '0')}",
                EquipSubClass = equipSubClass,
            });
            return Task.FromResult(lst);
        }

        public virtual void Dispose()
        {
        }
    }
}
