using ADOLib;

namespace WPFCore.Data.OleDb
{
    public interface IDBFactory
    {
        IDatabase NewDB();
    }

    internal class DBFactory : IDBFactory
    {
        private readonly string connString;

        public DBFactory(string connString)
        {
            this.connString = connString;
        }

        public IDatabase NewDB() => new DataDB(connString);

    }
}
