using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.Data;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;

namespace WPFCore.ElectGrid.TC
{
    public class UTabControlBinder
    {
        #region Init

        private IServiceProvider _provider = null!;

        UTabControl UTControl { get; set; } = null!;

        public void Init(UTabControl utc, IServiceProvider provider)
        {
            this._provider = provider;
            this.UTControl = utc;
            this.ConfigureCommands(utc);
        }

        #endregion

        #region Configure commands

        private void ConfigureCommands(UTabControl utc)
        {
            utc.CommandBindings.Add(new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted));
            utc.CommandBindings.Add(new(ApplicationCommands.Close, this.OnCloseTab, this.OnCloseTabCanExecuted));
        }

        #region ViewDetail command

        virtual protected void OnViewDetail(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                this.AddReport(eq.ID);
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

        #region CloseTab command

        virtual protected void OnCloseTab(object sender, ExecutedRoutedEventArgs e)
        {
            var tab = this.UTControl.SelectedItem;
            var tabs = this.UTControl.TControl.Items;
            var idx = tabs.IndexOf(tab);
            var nidx = idx - 1;
            if (nidx < 0)
                nidx = 0;
            tabs.Remove(tab);
            if (tab.Content is IDisposable obj)
                obj.Dispose();
            if (tabs.Count > 0 && tabs.GetItemAt(nidx) is TabItem t)
            {
                t.IsSelected = true;
            }
        }

        virtual protected void OnCloseTabCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.UTControl.SelectedItem != null)
            {
                e.CanExecute = true;
            }
            else { e.CanExecute = false; }
        }

        #endregion

        #endregion

        #region Report

        public void AddReport(string reportId)
        {
            var ti = this.UTControl.TControl.Items.Cast<ReportTabItem>().Where(i =>
                 i is ReportTabItem r && r.ID == reportId
            ).FirstOrDefault();

            if (ti != null)
                ti.IsSelected = true;
            else
            {
                ti = new ReportTabItem();
                ti.ShowReport(reportId, this.UTControl.TControl, this._provider);
            }
        }

        #endregion
    }
}
