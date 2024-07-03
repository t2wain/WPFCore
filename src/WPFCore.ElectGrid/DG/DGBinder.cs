using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.UI;
using WPFCore.ElectGrid.RPT;
using WPFCore.ElectGrid.TC;
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
                    DataGridConfig.SetDataGridOption(this.GridControl, VM2.ReportDef);
                    break;
                case nameof(VM2.Columns):
                    this.AddColumns(VM2.Columns);
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
            var c = new UReportDef();
            var vm = new UReportDefVM();
            vm.ReportDef = VM2.ReportDef;
            c.Init(vm);
            var res = DialogUtility.GetDialogWindow(c, $"Report Edit - {VM2.ReportDef!.Name}").ShowDialog();
        }

        #endregion
    }
}
