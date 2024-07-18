using System.Xml.Serialization;

namespace WPFCore.Data.Report
{
    [Serializable]
    public record ReportDefinition
    {
        public const string DB_TYPE_VIEW = "VIEW";
        public const string DB_TYPE_PROC = "PROC";
        public const string DB_TYPE_TABLE = "TABLE";
        public const string DB_TYPE_TABLE_EDIT = "TABLE_EDIT";
        public const string DB_TYPE_PROC_EDIT = "PROC_EDIT";

        [XmlIgnore]
        public int ReportID { get; set; }
        [XmlIgnore]
        public int CategoryID { get; set; }
        [XmlIgnore]
        public string? FileName { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DatabaseView { get; set; }

        public List<ColumnDefinition>? Columns { get; set; }
        public List<ColumnDefinition>? Parameters { get; set; }

        [XmlIgnore]
        public string? ReportDefinitionXml { get; set; }
        public string DatabaseObjectType { get; set; } = ReportDefinition.DB_TYPE_VIEW;
        public bool AllowAddAndDelete { get; set; }

        public string? UpdateDbProcedure { get; set; }
        public string? AddDbProcedure { get; set; }
        public string? DeleteDbProcedure { get; set; }
        public string? LookUpDbProcedure { get; set; }

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
            this.FileName = def.FileName;

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
