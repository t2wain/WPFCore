using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.UI;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.RPT;
using WPFCore.Shared.UI.DG;
using WPFCore.Shared.UI.DLG;

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
            this.ConfigureCommands(dg);
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

        #region Configure Commands 

        protected void ConfigureCommands(DataGrid dg)
        {
            dg.CommandBindings.Add(new(TACommands.Edit, this.OnEdit, this.OnEditCanExecuted));
        }

        protected void OnEditCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void OnEdit(object sender, ExecutedRoutedEventArgs e)
        {
            // Create a copy of report def
            var dlrdef = new ReportDefinition();
            dlrdef.SetData(VM2.ReportDef!);
            dlrdef.FileName = VM2.ReportDef!.FileName;

            // Update column def width based on datagrid column
            DataGridUtility.UpdateColumnDefWidth(dlrdef.Columns!, VM2.Columns);

            // Configure dialog window
            var c = new UReportDef();
            var dlvm = new UReportDefVM(VM2.ReportDS);
            dlvm.ReportDef = dlrdef;
            c.Init(dlvm);

            // Show dialog
            var dlres = DialogUtility.GetDialogWindow(c, $"Report Edit - {VM2.ReportDef!.Name}", null, 470).ShowDialog();

            // Update column defs
            if (dlres.HasValue && dlres.Value)
                VM2.UpdateReportDef(dlvm.ReportDef);
        }

        #endregion
    }
}
