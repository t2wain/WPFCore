using System.Xml.Serialization;

namespace WPFCore.Data.Report
{
    [Serializable]
    public class ReportDefinition
    {
        public const string DB_TYPE_VIEW = "VIEW";
        public const string DB_TYPE_PROC = "PROC";
        public const string DB_TYPE_TABLE = "TABLE";
        public const string DB_TYPE_TABLE_EDIT = "TABLE_EDIT";
        public const string DB_TYPE_PROC_EDIT = "PROC_EDIT";

        public ReportDefinition()
        {
            this.DatabaseObjectType = ReportDefinition.DB_TYPE_VIEW;
        }

        [XmlIgnore]
        public int ReportID;
        [XmlIgnore]
        public int CategoryID;
        public string? Name;
        public string? Description;
        public string? DatabaseView;

        public List<ColumnDefinition>? Columns;
        public List<ColumnDefinition>? Parameters;

        [XmlIgnore]
        public string? ReportDefinitionXml;
        public string? DatabaseObjectType;
        public bool AllowAddAndDelete;

        public string? UpdateDbProcedure;
        public string? AddDbProcedure;
        public string? DeleteDbProcedure;
        public string? LookUpDbProcedure;

        public Dictionary<string, object> GetSelectParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (this.Parameters != null)
                foreach (ColumnDefinition p in this.Parameters)
                    parameters.Add(p.FieldName!, p.Filter!);
            return parameters;
        }

        public void SetData(ReportDefinition def)
        {
            this.Name = def.Name;
            this.Description = def.Description;
            this.DatabaseView = def.DatabaseView;
            this.DatabaseObjectType = def.DatabaseObjectType;
            this.AllowAddAndDelete = def.AllowAddAndDelete;
            this.UpdateDbProcedure = def.UpdateDbProcedure;
            this.AddDbProcedure = def.AddDbProcedure;
            this.DeleteDbProcedure = def.DeleteDbProcedure;
            this.LookUpDbProcedure = def.LookUpDbProcedure;

            if (this.Columns == null)
                this.Columns = new List<ColumnDefinition>();
            else this.Columns.Clear();
            foreach (ColumnDefinition col in def.Columns!)
            {
                ColumnDefinition ncol = new ColumnDefinition();
                ncol.SetData(col);
                ncol.ReportID = this.ReportID;
                this.Columns.Add(ncol);
            }

            if (this.Parameters == null)
                this.Parameters = new List<ColumnDefinition>();
            else this.Parameters.Clear();
            foreach (ColumnDefinition col in def.Parameters!)
            {
                ColumnDefinition ncol = new ColumnDefinition();
                ncol.SetData(col);
                ncol.ReportID = this.ReportID;
                this.Parameters.Add(ncol);
            }

        }
    }

}
