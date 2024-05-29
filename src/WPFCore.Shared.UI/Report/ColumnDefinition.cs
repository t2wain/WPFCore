using System.Xml.Serialization;

namespace WPFCore.Shared.UI.Report
{
    [Serializable]
    public class ColumnDefinition
    {
        public const int ALIGN_LEFT = 0;
        public const int ALIGN_RIGHT = 1;
        public const int ALIGN_CENTER = 2;

        [XmlIgnore]
        public int ColumnID;
        [XmlIgnore]
        public int ReportID;

        private string _headerName = "";
        public string HeaderName
        {
            get { return this._headerName; }
            set { this._headerName = value; }
        }

        private string _fieldName = "";
        public string FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        private int _columnWidth = 0;
        public int ColumnWidth
        {
            get { return this._columnWidth; }
            set { this._columnWidth = value; }
        }

        private bool _visible = false;
        public bool Visible
        {
            get { return this._visible; }
            set { this._visible = value; }
        }

        private int _position = 0;
        public int Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        private string _format = "";
        public string Format
        {
            get { return this._format; }
            set { this._format = value; }
        }

        private int _alignment = 0;
        public int Alignment
        {
            get { return this._alignment; }
            set { this._alignment = value; }
        }

        private string _dataType = "";
        public string DataType
        {
            get { return this._dataType; }
            set { this._dataType = value; }
        }

        private string _filter = "";
        public string Filter
        {
            get { return this._filter; }
            set { this._filter = value; }
        }

        private bool _isEditable = false;
        public bool IsEditable
        {
            get { return this._isEditable; }
            set { this._isEditable = value; }
        }

        private bool _isFrozen = false;
        public bool IsFrozen
        {
            get { return this._isFrozen; }
            set { this._isFrozen = value; }
        }

        public bool IsLookUp { get; set; }

        public bool IsPrimaryKey { get; set; }

        public void SetData(ColumnDefinition col)
        {
            this.HeaderName = col.HeaderName;
            this.FieldName = col.FieldName;
            this.ColumnWidth = col.ColumnWidth;
            this.Visible = col.Visible;
            this.Position = col.Position;
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
