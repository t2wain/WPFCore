using ADOLib;

namespace WPFCore.Data.OleDb
{
    public class DataDB : AccessDatabase 
    {
        public DataDB(string connString)
        {
            this.ConnectionString = connString;
        }
    }
}
