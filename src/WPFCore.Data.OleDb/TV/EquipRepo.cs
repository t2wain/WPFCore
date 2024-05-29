using ADOLib;
using System.Data;
using WPFCore.Data.TV;

namespace WPFCore.Data.OleDb.TV
{
    public class EquipRepo : IEquipRepo
    {
        private readonly IDatabase _db;

        public EquipRepo(IDatabase db)
        {
            this._db = db;
        }

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

        public Task<IEnumerable<EquipItem>> GetEquipment(int equipSubClass)
        {
            string sql = $"select id, tag, equipclass, equipsubclass from vw_plantitem where equipsubclass = {equipSubClass} order by tag";
            return _db.ExecuteTableAsync(sql, "t1")
                .ContinueWith(t =>
                {
                    var dt = t.Result;
                    var lst = dt.Rows.Cast<DataRow>().Select(r => new EquipItem()
                    {
                        ID = r[0].ToString(),
                        Name = r[1].ToString(),
                        EquipClass = Convert.ToInt32(r[2]),
                        EquipSubClass = Convert.ToInt32(r[3]),
                    }).ToList().AsEnumerable();
                    return lst;
                });
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}
