using System.Windows;
using System.Windows.Input;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;
using WPFCore.Shared.UI.LB;
using NT = WPFCore.Common.ElectIndex.TIndexNodeEnum;

namespace WPFCore.ElectIndex.LB
{
    /// <summary>
    /// Setup event handlers and command bindings for the ListBox control
    /// and retrieve the view model from the DataContext.
    /// </summary>
    public class LBoxBinder : ListBoxBinder
    {
        protected override void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            base.OnDoubleClick(sender, e);
            this.RaiseViewDetailEvent();
        }

        protected LBoxVM VM2 => (LBoxVM)this.VM!; 

        protected void RaiseViewDetailEvent()
        {
            if (!this.IsCommandCanExecute(TACommands.ViewDetailMsg))
                return;

            var lst = this.VM!.SelectedItems.Cast<LBoxItemVM>()
                .Select(i => i.Data)
                .ToList();

            if (lst.First() is TNodeData n)
            {
                this.VM2.RaisePropertyChangeEvent(LBoxVM.ExecuteViewDetailCmdEvent);
                this.VM2.SendMessage(n);
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

        protected override IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>()
            {
                new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted)
            };
            lst.AddRange(base.GetCommandBindings());
            return lst;
        }

        protected override bool IsCommandCanExecute(string cmdName)
        {
            var allow = base.IsCommandCanExecute(cmdName);
            switch (cmdName)
            {
                case TACommands.ViewDetailMsg:
                    if (this.VM is ListBoxVM vm 
                        && vm.SelectedItems.Count() > 0
                        && vm.SelectedItems.First() is LBoxItemVM ivm 
                        && ivm.Data is TNodeData d)
                    {
                        switch (d.NodeType)
                        {
                            case NT.Motor:
                            case NT.Report:
                                allow = true;
                                break;
                        }
                    }
                    else allow = false;
                    break;
            }
            return allow;
        }

        #endregion

    }
}
