using ADOLib;
using WPFCore.Data.OleDb;
using WPFCore.Data.OleDb.TV;

namespace WPFCore.Test
{
    public class Context : IDisposable
    {
        public Context()
        {
            Repo = new EquipRepo(NewDB());
        }

        public EquipRepo Repo { get; init; }

        public IDatabase NewDB() => new DataDB("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\\devgit\\Data\\SPEL.accdb");

        public void Dispose()
        {
            Repo.Dispose();
        }
    }
}
