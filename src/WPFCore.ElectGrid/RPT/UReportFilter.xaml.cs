using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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

        UReportFilterVM VM { get; set; } = null!;

        public void Init(UReportFilterVM vm)
        {
            this.DataContext = vm;
            VM = vm;
            SetupReportColumnDG(_dgCol);
            ConfigCommands(_dgCol);
        }

        protected void SetupReportColumnDG(DataGrid dg)
        {
            dg.AutoGenerateColumns = false;
            dg.ItemsSource = VM.ReportColumns;
            dg.HorizontalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.VerticalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.CanUserAddRows = false;
            dg.CanUserDeleteRows = false;
            dg.FrozenColumnCount = 1;
            dg.CanUserResizeRows = false;
            dg.SelectionUnit = DataGridSelectionUnit.CellOrRowHeader;

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

        #region DataGrid paste

        void ConfigCommands(DataGrid dg)
        {
            var cb = new CommandBinding(ApplicationCommands.Paste, this.OnPaste, (s, e) => e.CanExecute = true);
            dg.CommandBindings.Add(cb);
        }

        void OnPaste(object sender, ExecutedRoutedEventArgs e)
        {
            var val = ParseData(Clipboard.GetText());
            DataGridUtility.PasteIntoRowsAndColumns(val, _dgCol, UpdateDataGridCellImpl);
            e.Handled = true;
        }

        string ParseData(string val)
        {
            var d = Regex.Replace(val, "\r\n$", "");
            var drows = d.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            return string.Join(";", drows.Distinct());
        }

        void UpdateDataGridCellImpl(object boundItem, DataGridColumn column, object data)
        {
            if (boundItem is UReportFilterVM.FilterColumn coldef 
                && column is DataGridTextColumn col 
                && col.Binding is Binding b 
                && b.Path.Path == "Filter")
            {
                coldef.Filter = data.ToString();
            }
        }

        #endregion
    }
}
