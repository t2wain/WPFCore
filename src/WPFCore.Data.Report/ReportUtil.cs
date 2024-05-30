using ADOLib;
using System.Data;
using System.Data.Common;
using System.Xml.Serialization;

namespace WPFCore.Data.Report
{
    public class ReportUtil
    {

        #region Report

        public virtual DataView LoadReport(IDatabase db, ReportDefinition report, string whereClause = "")
        {
            DbCommand cmd = null!;
            DataView vw = null!;
            using (cmd = db.CreateCommand())
            {
                switch (report.DatabaseObjectType)
                {
                    case ReportDefinition.DB_TYPE_VIEW:
                    case ReportDefinition.DB_TYPE_TABLE:
                    case ReportDefinition.DB_TYPE_TABLE_EDIT:
                        cmd.CommandText = String.Format("select * from {0} {1}",
                            report.DatabaseView, whereClause);
                        cmd.CommandType = CommandType.Text;
                        break;
                    case ReportDefinition.DB_TYPE_PROC:
                    case ReportDefinition.DB_TYPE_PROC_EDIT:
                        cmd.CommandText = report.DatabaseView;
                        cmd.CommandType = CommandType.StoredProcedure;
                        db.DeriveParameters(cmd);
                        this.SetParameterValues(db, cmd.Parameters, report.GetSelectParameters());
                        break;
                }
                vw = db.ExecuteTable(cmd, report.DatabaseView!).DefaultView;
            }

            return vw;
        }

        protected void SetParameterValues(IDatabase db, DbParameterCollection parameters, IDictionary<string, object> paramValues)
        {
            foreach (DbParameter p in parameters)
            {
                string paramName = db.GetCommonParamName(p.ParameterName);
                if ((p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput)
                    && paramValues.ContainsKey(paramName))
                    p.Value = paramValues[paramName];
            }

        }

        #endregion

        #region Definition

        // for new report that has not been setup
        public ReportDefinition GetReportDefinition(IDatabase db, DataView dv)
        {
            ReportDefinition def = new ReportDefinition();
            def.DatabaseView = dv.Table!.TableName;
            def.Columns = this.GetColumns(dv);
            def.Parameters = this.GetUpdatedParameters(db, def);
            return def;
        }

        private List<ColumnDefinition> GetColumns(DataView dv)
        {
            List<ColumnDefinition> lstColumns = new List<ColumnDefinition>();
            DataTable t = dv.Table!;
            foreach (DataColumn c in t.Columns)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.ColumnID = c.Ordinal;
                col.FieldName = c.ColumnName;
                col.HeaderName = c.ColumnName;
                col.Position = c.Ordinal;
                col.DataType = c.DataType.ToString();
                col.ColumnWidth = 100;
                col.Alignment = ColumnDefinition.ALIGN_LEFT;
                col.Visible = true;
                lstColumns.Add(col);
            }
            return lstColumns;
        }

        public List<ColumnDefinition> GetUpdatedColumnDefinitions(IDatabase db, ReportDefinition oldDef, DataView newDV)
        {
            ReportDefinition newDef = this.GetReportDefinition(db, newDV);
            if (newDef.Columns == null || newDef.Columns.Count == 0)
                newDef.Columns = this.GetUpdatedColumnDefinitionsFromDB(db, oldDef);

            List<ColumnDefinition> lstUpdateColumns = new List<ColumnDefinition>();

            Dictionary<string, ColumnDefinition> oldColumns =
                new Dictionary<string, ColumnDefinition>();
            foreach (ColumnDefinition oldCol in oldDef.Columns)
                oldColumns.Add(oldCol.FieldName, oldCol);

            int pos = 0;
            foreach (ColumnDefinition newCol in newDef.Columns)
            {
                if (oldColumns.ContainsKey(newCol.FieldName))
                    lstUpdateColumns.Add(oldColumns[newCol.FieldName]);
                else
                {
                    newCol.ReportID = oldDef.ReportID;
                    lstUpdateColumns.Add(newCol);
                }
                newCol.Position = pos++;
            }

            return lstUpdateColumns;
        }

        private List<ColumnDefinition> GetUpdatedColumnDefinitionsFromDB(IDatabase db, ReportDefinition def)
        {
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            using (DbCommand cmd = db.CreateCommand())
            {
                switch (def.DatabaseObjectType)
                {
                    case ReportDefinition.DB_TYPE_PROC:
                    case ReportDefinition.DB_TYPE_PROC_EDIT:
                        cmd.CommandText = def.DatabaseView;
                        cmd.CommandType = CommandType.StoredProcedure;
                        break;
                    default:
                        cmd.CommandText = String.Format("select * from {0}", def.DatabaseView);
                        cmd.CommandType = CommandType.Text;
                        break;
                }

                if (cmd.CommandText != String.Empty)
                {
                    DbDataAdapter da = db.CreateDataAdapter();
                    da.SelectCommand = cmd;
                    if (cmd.CommandType == CommandType.StoredProcedure)
                        db.DeriveParameters(cmd);
                    cmd.Connection = db.Connection;
                    DataTable tbl = new DataTable("schema");
                    da.FillSchema(tbl, SchemaType.Source);
                    columns = this.GetColumnDefinitions(tbl);
                }
            }
            return columns;
        }

        protected List<ColumnDefinition> GetColumnDefinitions(DataTable table)
        {
            List<ColumnDefinition> lstCols = new List<ColumnDefinition>();
            foreach (DataColumn col in table.Columns)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.ColumnID = col.Ordinal;
                cd.FieldName = col.ColumnName;
                cd.HeaderName = cd.FieldName;
                cd.Position = cd.ColumnID;
                cd.DataType = col.DataType.ToString();
                cd.ColumnWidth = 100;
                cd.Alignment = ColumnDefinition.ALIGN_LEFT;
                cd.Visible = true;
                lstCols.Add(cd);
            }
            return lstCols;
        }

        public List<ColumnDefinition> GetUpdatedParameters(IDatabase db, ReportDefinition def)
        {
            bool cont = false;
            switch (def.DatabaseObjectType)
            {
                case ReportDefinition.DB_TYPE_PROC:
                case ReportDefinition.DB_TYPE_PROC_EDIT:
                    cont = true;
                    break;
            }
            if (!cont) return null;

            List<ColumnDefinition> newParameters = new List<ColumnDefinition>();
            using (IDbCommand cmd = db.CreateCommand())
            {
                
                cmd.CommandText = def.DatabaseView;
                cmd.CommandType = CommandType.StoredProcedure;
                db.DeriveParameters(cmd);

                foreach (IDbDataParameter p in cmd.Parameters)
                {
                    if (p.Direction == ParameterDirection.Input
                        || p.Direction == ParameterDirection.InputOutput)
                    {
                        ColumnDefinition c = new ColumnDefinition()
                        {
                            FieldName = p.ParameterName,
                            DataType = p.DbType.ToString()
                        };
                        newParameters.Add(c);
                    }
                }
            }

            Dictionary<string, ColumnDefinition> oldParameters =
                new Dictionary<string, ColumnDefinition>();
            if (def.Parameters != null)
                foreach (ColumnDefinition oldCol in def.Parameters)
                    oldParameters.Add(oldCol.FieldName, oldCol);

            List<ColumnDefinition> lstUpdateParameters = new List<ColumnDefinition>();
            foreach (ColumnDefinition newParameter in newParameters)
            {
                if (oldParameters.ContainsKey(newParameter.FieldName))
                    lstUpdateParameters.Add(oldParameters[newParameter.FieldName]);
                else
                {
                    newParameter.ReportID = def.ReportID;
                    lstUpdateParameters.Add(newParameter);
                }
            }

            return lstUpdateParameters;
        }

        protected string SerializeReportDefinition(ReportDefinition def)
        {
            XmlSerializer s = new XmlSerializer(typeof(ReportDefinition));
            TextWriter w = new StringWriter();
            s.Serialize(w, def);
            return w.ToString();
        }

        protected ReportDefinition DeserializeReportDefinition(string xml)
        {
            XmlSerializer s = new XmlSerializer(typeof(ReportDefinition));
            TextReader r = new StringReader(xml);
            ReportDefinition def = (ReportDefinition)s.Deserialize(r);
            return def;
        }

        public List<string> LoadLookUp(IDatabase db, string dbLookupProc, string colName)
        {
            List<string> lst = new List<string>();
            DataTable t = new DataTable(colName);
            using (IDbCommand cmd = db.CreateCommand(dbLookupProc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                db.DeriveParameters(cmd);
                foreach (DbParameter p in cmd.Parameters)
                    if (p.Direction == ParameterDirection.Input)
                    {
                        p.Value = colName;
                        break;
                    }
                db.ExecuteTable(cmd, t);
            }

            foreach (DataRow r in t.Rows)
                lst.Add(r[0].ToString());

            return lst;
        }

        #endregion
    }
}
