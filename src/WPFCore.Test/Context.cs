using WPFCore.Data;
using WPFCore.Data.TV;

namespace WPFCore.Test
{
    public class Context : IDisposable
    {
        public Context()
        {
            Repo = new EquipRepo(new DataDB("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\\devgit\\Data\\SPEL.accdb"));
        }

        public EquipRepo Repo { get; init; }

        public void Dispose()
        {
            Repo.Dispose();
        }
    }
}
