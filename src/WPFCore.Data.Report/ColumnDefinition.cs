using System.Xml.Serialization;

namespace WPFCore.Data.Report
{
    [Serializable]
    public record ColumnDefinition
    {
        public const int ALIGN_LEFT = 0;
        public const int ALIGN_RIGHT = 1;
        public const int ALIGN_CENTER = 2;

        [XmlIgnore]
        public int ColumnID { get; set; }
        [XmlIgnore]
        public int ReportID { get; set; }

        public string? HeaderName { get; set; }

        public string? FieldName { get; set; }

        public string? Description { get; set; }

        public int ColumnWidth { get; set; }

        public bool Visible { get; set; }

        public int Position { get; set; }

        public string? Format { get; set; }

        public int Alignment { get; set; }

        public string? DataType { get; set; }

        public string? Filter { get; set; }

        public bool IsEditable { get; set; }

        public bool IsFrozen { get; set; }

        public bool IsLookUp { get; set; }

        public bool IsPrimaryKey { get; set; }

        public void SetData(ColumnDefinition col)
        {
            this.HeaderName = col.HeaderName;
            this.FieldName = col.FieldName;
            this.Description = col.Description;
            this.ColumnWidth = col.ColumnWidth;
            this.Visible = col.Visible;
            this.Position = col.Position;
            this.Format = col.Format;
            this.Alignment = col.Alignment;
            this.DataType = col.DataType;
            this.Filter = col.Filter;
            this.IsEditable = col.IsEditable;
            this.IsFrozen = col.IsFrozen;
            this.IsLookUp = col.IsLookUp;
            this.IsPrimaryKey = col.IsPrimaryKey;
        }
    }

}
