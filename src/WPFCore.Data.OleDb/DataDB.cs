using ADOLib;

namespace WPFCore.Data.OleDb
{
    internal class DataDB : OleDbDatabase 
    {
        public DataDB(string connString)
        {
            this.ConnectionString = connString;
        }
    }
}
