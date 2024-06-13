using System.Windows;
using System.Windows.Input;
using WPFCore.Common.UI;
using WPFCore.Shared.UI.TV;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// Setup event handlers and command bindings for the TreeView control
    /// and retrieve the view model from the DataContext.
    /// </summary>
    public class TreeIndexBinder : TreeViewBinder
    {
        protected override void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            base.OnDoubleClick(sender, e);

            if (this.VM?.SelectedItem is NodeVM n) 
            {
                if (n.IsLeafNode)
                    this.RaiseViewDetailEvent();
            }

        }

        protected void RaiseViewDetailEvent()
        {
            if (this.VM is TreeVM vm 
                && vm.SelectedItem is NodeVM n 
                && vm.IsCommandCanExecute(TACommands.ViewDetailMsg, n)) {

                vm.SendMessage(n.DataItem!);
                vm.RaisePropertyChangeEvent(TreeVM.ExecuteViewDetailCmdEvent);
            }

        }

        #region Config Commands

        virtual protected void OnViewDetail(object sender, RoutedEventArgs e)
        {
            this.RaiseViewDetailEvent();
        }

        virtual protected void OnViewDetailCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TACommands.ViewDetailMsg);
        }

        // command handler for Filter
        virtual protected void OnSetFilter(object sender, RoutedEventArgs e)
        {
            var node = this.VM?.SelectedItem as NodeVM;
            if (node == null)
                return;

            //PlanPackageFilter pkgFilter = null;
            //BOMFilter bomFilter = null;
            //ProgressFilter progFilter = null;
            //bool? result = null;
            //var du = new FilterUtility();
            //switch (node.NodeType)
            //{
            //    case NT.Packages:
            //        result = du.ShowPackageFilter(node.Filter.PackageFilter, out pkgFilter);
            //        break;
            //    case NT.BOMs:
            //        result = du.ShowBomFilter(node.Filter.BOMFilter, out bomFilter);
            //        if (result.Value)
            //        {
            //            node.Filter.BOMFilter = bomFilter;
            //            node.RefreshData();
            //        }
            //        break;
            //    case NT.ProgressDocs:
            //        result = du.ShowProgressFilter(node.Filter.ProgressFilter, out progFilter);
            //        if (result.Value)
            //        {
            //            node.Filter.ProgressFilter = progFilter;
            //            node.RefreshData();
            //        }
            //        break;
            //}
        }

        virtual protected void OnSetFilterCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TACommands.SetFilterMsg);
        }

        /// <summary>
        /// Setup command bindings
        /// </summary>
        protected override IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>()
            {
                new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted),
                new (TACommands.SetFilter, this.OnSetFilter, this.OnSetFilterCanExecuted)
            };
            lst.AddRange(base.GetCommandBindings());
            return lst;
        }

        #endregion

    }
}
