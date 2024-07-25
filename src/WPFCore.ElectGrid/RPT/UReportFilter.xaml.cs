using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.DG;
using R = WPFCore.Data.Report;

namespace WPFCore.ElectGrid.RPT
{
    /// <summary>
    /// Interaction logic for UReportFilter.xaml
    /// </summary>
    public partial class UReportFilter : UserControl
    {
        public UReportFilter()
        {
            InitializeComponent();
        }

        public void Init(UReportDefVM vm)
        {
            this.DataContext = vm;
            SetupReportColumnDG(_dgCol, vm.ReportDef!.Columns!);
        }

        protected void SetupReportColumnDG(DataGrid dg, IEnumerable<R.ColumnDefinition> coldefs)
        {
            dg.AutoGenerateColumns = false;
            dg.ItemsSource = coldefs;
            dg.HorizontalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.VerticalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.CanUserAddRows = false;
            dg.CanUserDeleteRows = false;
            dg.FrozenColumnCount = 1;
            dg.CanUserResizeRows = false;

            var cols = new List<DataGridColumn>()
            {
                DataGridUtility.CreateTextColumn("FieldName", "Field Name", 150),
                DataGridUtility.CreateTextColumn("HeaderName", "Header Name", 150),
                DataGridUtility.CreateTextColumn("Description", "Description", 200),
                DataGridUtility.CreateTextColumn("Filter", "Filter", 150, false, markEditable: false),
            };

            foreach (var col in cols)
                dg.Columns.Add(col);
        }

    }
}
