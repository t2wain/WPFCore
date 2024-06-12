using System.Data;

namespace WPFCore.Data.OleDb.TV
{
    internal class EquipRepo : WPFCore.Data.TV.EquipRepo
    {
        private readonly IDBFactory _dbfact;

        public EquipRepo(IDBFactory dbfact)
        {
            this._dbfact = dbfact;
        }

        public override Task<IEnumerable<EquipItem>> GetEquipment(int equipSubClass)
        {
            string sql = $"select id, tag, equipclass, equipsubclass from vw_plantitem where equipsubclass = {equipSubClass} order by tag";
            var db = _dbfact.NewDB();
            return db.ExecuteTableAsync(sql, "t1")
                .ContinueWith(t =>
                {
                    db.Dispose();
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
    }
}
