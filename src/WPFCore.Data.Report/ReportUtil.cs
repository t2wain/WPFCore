using ADOLib;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace WPFCore.Data.Report
{
    public static class ReportUtil
    {

        #region Run Report

        /// <summary>
        /// Query the database to return the DataView
        /// based on the ReportDefinition configuration
        /// </summary>
        public static DataView LoadReport(IDatabase db, ReportDefinition report, string whereClause = "")
        {
            using var cmd = db.CreateCommand();
            switch (report.DatabaseObjectType)
            {
                case ReportDefinition.DB_TYPE_VIEW:
                case ReportDefinition.DB_TYPE_TABLE:
                case ReportDefinition.DB_TYPE_TABLE_EDIT:
                    cmd.CommandText = String.Format("select * from {0} {1}",
                        report.DatabaseView, whereClause);
                    cmd.CommandType = CommandType.Text;
                    SQLChecker.ValidateSafeReader(cmd.CommandText);
                    break;
                case ReportDefinition.DB_TYPE_PROC:
                case ReportDefinition.DB_TYPE_PROC_EDIT:
                    cmd.CommandText = report.DatabaseView;
                    cmd.CommandType = CommandType.StoredProcedure;
                    db.DeriveParameters(cmd);
                    SetParameterValues(db, cmd.Parameters, report.GetSelectParameters());
                    break;
            }

            return db.ExecuteTable(cmd, report.DatabaseView!).DefaultView;
        }

        public static Task<DataView> LoadReportAsync(IDatabase db, ReportDefinition report, string whereClause = "") =>
            Task.Factory.StartNew(() => LoadReport(db, report, whereClause));

        /// <summary>
        /// Set the DbCommand parameters based
        /// on the ReportDefinition configuration
        /// </summary>
        static void SetParameterValues(IDatabase db, DbParameterCollection parameters, 
            IDictionary<string, object> paramValues)
        {
            foreach (DbParameter p in parameters)
            {
                string paramName = db.GetCommonParamName(p.ParameterName);
                if ((p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput)
                    && paramValues.ContainsKey(paramName))
                    p.Value = paramValues[paramName];
            }

        }

        public static List<string> LoadLookUp(IDatabase db, string dbLookupProc, string colName)
        {
            using var cmd = db.CreateCommand(dbLookupProc);
            cmd.CommandType = CommandType.StoredProcedure;
            db.DeriveParameters(cmd);
            foreach (DbParameter p in cmd.Parameters)
            {
                if (p.Direction == ParameterDirection.Input)
                {
                    p.Value = colName;
                    break;
                }
            }

            var tbl = db.ExecuteTable(cmd, "table1");
            var lst = new List<string>();
            foreach (DataRow r in tbl.Rows)
                lst.Add(r[0].ToString()!);
            return lst;
        }

        public static Task<List<string>> LoadLookUpAsync(IDatabase db, string dbLookupProc, string colName) =>
            Task.Factory.StartNew<List<string>>(() => LoadLookUp(db, dbLookupProc, colName));

        #endregion

        #region Create Definition

        /// <summary>
        /// Create a list of ColumnDefinition
        /// based on the columns in the DataView
        /// </summary>
        public static List<ColumnDefinition> GetColumns(DataView dv)
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

        /// <summary>
        /// Get a matching old column definition
        /// or the new column definition
        /// </summary>
        public static List<ColumnDefinition> GetUpdatedColumnDefinitions(IEnumerable<ColumnDefinition> newCols,
            IEnumerable<ColumnDefinition> oldCols)
        {
            var cols = newCols.GroupJoin(
                oldCols,
                nc => nc.FieldName,
                oc => oc.FieldName,
                (nc, ocols) => ocols.FirstOrDefault() ?? nc
            ).ToList();

            return cols;
        }

        /// <summary>
        /// Get a matching old column definition
        /// or the new column definition
        /// </summary>
        public static List<ColumnDefinition> GetUpdatedColumnDefinitions(IDatabase db, ReportDefinition def)
        {
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            using var cmd = db.CreateCommand();
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
                using DbDataAdapter da = db.CreateDataAdapter();
                da.SelectCommand = cmd;
                if (cmd.CommandType == CommandType.StoredProcedure)
                    db.DeriveParameters(cmd);
                cmd.Connection = db.Connection;
                DataTable tbl = new DataTable("schema");
                da.FillSchema(tbl, SchemaType.Source);
                columns = GetColumnDefinitionsFromSchemaTable(tbl);
                if (def.Columns != null)
                    columns = GetUpdatedColumnDefinitions(columns, def.Columns);
            }
            
            return columns;
        }

        public static Task<List<ColumnDefinition>> GetUpdatedColumnDefinitionsAsync(IDatabase db, ReportDefinition def) =>
            Task.Factory.StartNew(() => GetUpdatedColumnDefinitions(db, def));

        static List<ColumnDefinition> GetColumnDefinitionsFromSchemaTable(DataTable table)
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

        public static List<ColumnDefinition>? GetUpdatedParameters(IDatabase db, ReportDefinition def)
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
                    oldParameters.Add(oldCol.FieldName!, oldCol);

            List<ColumnDefinition> lstUpdateParameters = new List<ColumnDefinition>();
            foreach (ColumnDefinition newParameter in newParameters)
            {
                if (oldParameters.ContainsKey(newParameter.FieldName!))
                    lstUpdateParameters.Add(oldParameters[newParameter.FieldName!]);
                else
                {
                    newParameter.ReportID = def.ReportID;
                    lstUpdateParameters.Add(newParameter);
                }
            }

            return lstUpdateParameters;
        }

        #endregion

        #region Serialization

        public static Task SaveReportDefinition(ReportDefinition reportDef)
        {
            var xml = SerializeReportDefinition(reportDef);
            return File.WriteAllTextAsync(reportDef.FileName!, xml);
        }

        public static string SerializeReportDefinition(ReportDefinition def)
        {
            var s = new XmlSerializer(typeof(ReportDefinition));
            var w = new StringWriter();
            s.Serialize(w, def);
            return w.ToString()!;
        }

        public static Task<ReportDefinition> DeserializeReportDefinitionFromFile(string filePath) =>
            File.ReadAllTextAsync(filePath)
                .ContinueWith(t =>
                {
                    var reportDef = DeserializeReportDefinition(t.Result);
                    reportDef.FileName = filePath;
                    return reportDef;
                });

        public static ReportDefinition DeserializeReportDefinition(string xml)
        {
            var s = new XmlSerializer(typeof(ReportDefinition));
            var r = new StringReader(xml);
            var def = (ReportDefinition)s.Deserialize(r)!;
            return def;
        }

        public static Task<ReportDefinition[]>? DeserializeReportDefinitionFromFolder(string folderPath) =>
            Task.WhenAll(Directory.EnumerateFiles(folderPath, "*.xml", SearchOption.TopDirectoryOnly)
                .Select(DeserializeReportDefinitionFromFile));


        #endregion

        #region Filter

        public static IEnumerable<string> GetWhereClause(IEnumerable<ColumnDefinition> lstColumn)
        {
            var lstWhere = new List<string>();
            var q = lstColumn.Where(c => !string.IsNullOrWhiteSpace(c.Filter));
            foreach (ColumnDefinition col in q)
            {
                if (Regex.IsMatch(col.Filter!, @"[\*]+"))
                    lstWhere.Add(GetExpressionSQL(col));
                else lstWhere.Add(GetStringSQL(col));
            }
            return lstWhere;
        }

        internal static string GetStringSQL(ColumnDefinition col)
        {
            var fieldName = col.FieldName!;
            var delimiter = "";
            if (col.DataType!.Equals("System.String",
                    StringComparison.InvariantCultureIgnoreCase))
                delimiter = "'";
            string[] vals = col.Filter!.Trim().Split(';');
            var lst = new List<string>();
            foreach (string val in vals)
            {
                //if (!SQLChecker.ValidateSafeReader(val))
                //    continue;

                var v = "";
                if (val.Contains("%"))
                    v = String.Format("{0} like {2}{1}{2}", fieldName, val.Trim(), delimiter);
                else if (val.Equals("<empty>", StringComparison.InvariantCultureIgnoreCase))
                    v = String.Format("{0} is null", fieldName);
                else if (val.Equals("<not_empty>", StringComparison.InvariantCultureIgnoreCase))
                    v = String.Format("{0} is not null", fieldName);
                else v = String.Format("{0} = {2}{1}{2}", fieldName, val.Trim(), delimiter);

                if (!string.IsNullOrWhiteSpace(val))
                    lst.Add(v);
            }
            return String.Format("({0})", String.Join(" or ", lst.ToArray()));
        }

        internal static string GetExpressionSQL(ColumnDefinition col)
        {
            var val = col.Filter!.Trim();
            val = val.Replace("*", col.FieldName);
            return String.Format("({0})", val);
        }
        #endregion
    }
}
