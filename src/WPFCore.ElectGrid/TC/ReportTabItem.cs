using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.UI;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.DG;
using WPFCore.ElectGrid.LV;
using WPFCore.Shared.UI.TV;

namespace WPFCore.ElectGrid.TC
{
    public class ReportTabItem : TabItem, IDisposable
    {
        public ReportTabItem() : base()
        {
            this.ConfigureCommands(this);
        }

        #region Data

        public string ID { get; set; } = null!;

        #endregion

        #region Show report

        internal async Task ShowReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);

            // for testing
            var production = false;
            IReportTabItemContent? urpt = null;

            if (production)
            {
                switch (reportDef.DatabaseObjectType)
                {
                    case ReportDefinition.DB_TYPE_VIEW:
                    case ReportDefinition.DB_TYPE_TABLE:
                    case ReportDefinition.DB_TYPE_PROC:
                        urpt = new UListView();
                        break;
                    case ReportDefinition.DB_TYPE_TABLE_EDIT:
                    case ReportDefinition.DB_TYPE_PROC_EDIT:
                        urpt = new UDataGridView();
                        break;
                }
            }
            else
            {
                urpt = new UDataGridView();
            }

            if (urpt != null)
                await ShowReport(reportId, utc, provider, urpt, reportDef);
        }

        IReportTabItemContent _urpt = null!;
        internal async Task ShowReport(string reportId, TabControl utc,
            IServiceProvider provider, IReportTabItemContent urpt, ReportDefinition? rdef = null)
        {
            this.ID = reportId;
            _urpt = urpt;

            var reportDef = rdef;
            if (reportDef == null)
                reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);

            this.Header = reportDef.Name;
            this.Content = urpt;
            utc.Items.Add(this);
            this.IsSelected = true;

            await urpt.ShowReport(reportDef, provider);
        }

        public int ItemCount => _urpt.ItemCount;

        #endregion

        #region Configure Commands 

        protected void ConfigureCommands(TabItem tab)
        {
            tab.CommandBindings.Add(new(TACommands.Edit, this.OnEdit, this.OnCommandCanExecuted));
            tab.CommandBindings.Add(new(TNCommands.Refresh, this.OnRefresh, this.OnCommandCanExecuted));
            tab.CommandBindings.Add(new(TACommands.SetFilter, this.OnSetFilter, this.OnCommandCanExecuted));
            tab.CommandBindings.Add(new(TACommands.ClearFilter, this.OnClearFilter, this.OnCommandCanExecuted));
        }

        protected void OnCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void OnEdit(object sender, ExecutedRoutedEventArgs e)
        {
            if (_urpt != null)
            {
                e.Handled = true;
                _urpt.ShowEditWindow();
            }
        }

        protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            if (_urpt != null)
            {
                e.Handled = true;
                _urpt.RefreshData();
            }
            //if (this.DGVM != null)
            //{
            //    e.Handled = true;
            //    this.DGVM.RefreshData();
            //}
            //else if (this.VM != null)
            //{
            //    e.Handled = true;
            //    this.VM.RefreshData();
            //}
        }

        internal Task RefreshData()
        {
            if (_urpt != null)
            {
                return _urpt.RefreshData();
            }
            else return Task.CompletedTask;
        }

        protected void OnSetFilter(object sender, ExecutedRoutedEventArgs e)
        {
            if (_urpt != null)
            {
                e.Handled = true;
                _urpt.SetFilter();
            }
        }

        protected void OnClearFilter(object sender, ExecutedRoutedEventArgs e)
        {
            if (_urpt != null)
            {
                e.Handled = true;
                _urpt.ClearFilter();
            }
        }

        #endregion

        public void Dispose()
        {
            if (this.Content is IDisposable obj)
                obj.Dispose();
        }
    }
}
