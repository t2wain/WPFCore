using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Common.UI;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.RPT;
using WPFCore.Shared.UI.DG;

namespace WPFCore.ElectGrid.DG
{
    public class DGBinder : DataGridBinder
    {
        #region Init

        public override void InitDataGrid(DataGrid dg, DataGridVM vm)
        {
            base.InitDataGrid(dg, vm);
            ConfigureEvent(dg);
        }

        DGridVM VM2 => (DGridVM)VM;

        protected void AddColumns(IEnumerable<DataGridColumn> cols) 
        {
            var dgcols = this.GridControl.Columns;
            dgcols.Clear();
            foreach (var c in cols)
                dgcols.Add(c);
        }

        public override void Dispose()
        {
            this.GridControl.GotFocus -= this.OnFocus;
            this.VM.PropertyChanged -= OnPropertyChanged;
            base.Dispose();
        }

        #endregion

        #region Configure events

        void ConfigureEvent(DataGrid dg)
        {
            dg.GotFocus += this.OnFocus;
            this.VM.PropertyChanged += OnPropertyChanged;
        }

        void OnFocus(object sender, RoutedEventArgs e)
        {
            this.GridControl.RaiseEvent(new(WPFCoreApp.CustomControlFocusEvent, this));
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(VM2.ReportDef):
                    DataGridConfig.SetDataGridOption(this.GridControl, VM2.ReportDef!);
                    break;
                case nameof(VM2.Columns):
                    this.AddColumns(VM2.Columns);
                    DataGridConfig.SetFrozenColumn(this.GridControl, VM2.ReportDef!);
                    break;
            }

        }

        #endregion

        internal void ShowEditWindow()
        {
            // Create a copy of report def
            var dlrdef = new ReportDefinition();
            dlrdef.SetData(VM2.ReportDef!);
            dlrdef.FileName = VM2.ReportDef!.FileName;

            // Update column def width based on datagrid column
            DataGridUtility.UpdateColumnDefWidth(dlrdef.Columns!, VM2.Columns);

            var success = ReportDialogUtility.ShowEditWindow(ref dlrdef, VM2.ReportDS);
            if (success)
                VM2.UpdateReportDef(dlrdef);
        }

        internal void ShowFilterWindow()
        {
            // Create a copy of report def
            var dlrdef = new ReportDefinition();
            dlrdef.SetData(VM2.ReportDef!);

            var success = ReportDialogUtility.ShowFilterWindow(ref dlrdef);
            if (success)
                VM2.SetFilter(dlrdef);
        }

        internal void ClearFilter()
        {
            VM2.ClearFiler();
        }

    }
}
