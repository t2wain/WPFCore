using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.Data;
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

        public LViewVM VM { get; set; } = null!;
        public DGridVM DGVM { get; set; } = null!;

        public string ID { get; set; } = null!;

        #endregion

        #region Show report

        internal async Task ShowReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);

            // for testing
            var production = false;
            if (production)
            {
                switch (reportDef.DatabaseObjectType)
                {
                    case ReportDefinition.DB_TYPE_VIEW:
                    case ReportDefinition.DB_TYPE_TABLE:
                    case ReportDefinition.DB_TYPE_PROC:
                        await ShowListViewReport(reportId, utc, provider, reportDef);
                        break;
                    case ReportDefinition.DB_TYPE_TABLE_EDIT:
                    case ReportDefinition.DB_TYPE_PROC_EDIT:
                        await ShowDataGridReport(reportId, utc, provider, reportDef);
                        break;
                }
            }
            else
            {
                //ShowListViewReport(reportId, utc, provider, reportDef);
                await ShowDataGridReport(reportId, utc, provider, reportDef);
            }

        }

        UListView _lvru = null!;
        internal async Task ShowListViewReport(string reportId, TabControl utc, 
            IServiceProvider provider, ReportDefinition? rdef = null)
        {
            var lvm = provider.GetRequiredService<LViewVM>();
            this.VM = lvm;
            this.ID = reportId;
            _lvru = new UListView();
            _lvru.Init(lvm);

            var reportDef = rdef;
            if (reportDef == null)
                reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            this.Header = reportDef.Name;
            this.Content = _lvru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await lvm.ShowReport(reportDef);
        }

        UDataGridView _dgru = null!;
        internal async Task ShowDataGridReport(string reportId, TabControl utc, 
            IServiceProvider provider, ReportDefinition? rdef = null)
        {
            var ds = provider.GetRequiredService<IReportDS>();

            var reportDef = rdef;
            if (reportDef == null)
                reportDef = await ds.GetReportDefinition(reportId);

            var dgvm = provider.GetRequiredService<DGridVM>();
            this.DGVM = dgvm;
            this.ID = reportId;
            _dgru = new UDataGridView();
            _dgru.Init(dgvm);

            this.Header = reportDef.Name;
            this.Content = _dgru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await dgvm.ShowReport(reportDef);
        }

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
            if (_dgru != null)
            {
                e.Handled = true;
                _dgru.ShowEditWindow();
            }
        }

        protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            if (this.DGVM != null)
            {
                e.Handled = true;
                this.DGVM.RefreshData();
            }
            else if (this.VM != null)
            {
                e.Handled = true;
                this.VM.RefreshData();
            }
        }

        protected void OnSetFilter(object sender, ExecutedRoutedEventArgs e)
        {
            if (_dgru != null)
            {
                e.Handled = true;
                _dgru.ShowFilterWindow();
            }
        }

        protected void OnClearFilter(object sender, ExecutedRoutedEventArgs e)
        {
            if (_dgru != null)
            {
                e.Handled = true;
                _dgru.ClearFilter();
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
