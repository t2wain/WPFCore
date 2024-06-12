using ADOLib;

namespace WPFCore.Data.OleDb
{
    internal class DataDB : AccessDatabase 
    {
        public DataDB(string connString)
        {
            this.ConnectionString = connString;
        }
    }
}
