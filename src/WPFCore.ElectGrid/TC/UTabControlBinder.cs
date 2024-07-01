using System.Windows.Input;
using WPFCore.Common.Data;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;

namespace WPFCore.ElectGrid.TC
{
    public class UTabControlBinder : IDisposable
    {
        private IServiceProvider _provider = null!;

        UTabControl UTControl { get; set; } = null!;

        public void Init(UTabControl utc, IServiceProvider provider)
        {
            this._provider = provider;
            this.UTControl = utc;
            this.ConfigureCommands(utc);
        }

        #region Configure commands

        private void ConfigureCommands(UTabControl utc)
        {
            utc.CommandBindings.Add(new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted));
        }

        virtual protected void OnViewDetail(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                this.AddReport(eq.ID, true);
            }
        }

        virtual protected void OnViewDetailCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                e.CanExecute = true;
            }
            else { e.CanExecute = false; }
        }

        #endregion

        public void AddReport(string reportId, bool useDataGrid = false)
        {
            var ti = this.UTControl.TControl.Items.Cast<ReportTabItem>().Where(i =>
                 i is ReportTabItem r && r.ID == reportId
            ).FirstOrDefault();

            if (ti != null)
                ti.IsSelected = true;
            else
            {
                ti = new ReportTabItem();
                if (useDataGrid)
                    ti.ShowDataGridReport(reportId, this.UTControl.TControl, this._provider);
                else ti.ShowListViewReport(reportId, this.UTControl.TControl, this._provider);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
