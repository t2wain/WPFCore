using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WPFCore.Data.Report;
using WPFCore.Shared.UI.DLG;

namespace WPFCore.ElectGrid.RPT
{
    public partial class UReportFilterVM : DialogVM
    {

        #region Filter column

        public partial class FilterColumn : ObservableObject
        {
            private readonly ColumnDefinition _col;

            public FilterColumn(ColumnDefinition col)
            {
                this._col = col;
            }

            public string? HeaderName 
            { 
                get => _col.HeaderName;
                set =>  throw new NotImplementedException();
            }

            public string? FieldName
            {
                get => _col.FieldName;
                set => throw new NotImplementedException();
            }

            public string? Description
            {
                get => _col.Description;
                set => throw new NotImplementedException();
            }

            public string? Filter 
            { 
                get => _col.Filter;
                set => SetProperty(_col.Filter, value, v => _col.Filter = v);
            }

        }

        #endregion

        public UReportFilterVM(ReportDefinition rdef) : base()
        {
            var q = rdef.Columns!.Select(c => new FilterColumn(c));
            this.ReportColumns = new ObservableCollection<FilterColumn>(q);
        }

        [ObservableProperty]
        ObservableCollection<FilterColumn> _reportColumns = null!;
    }
}
