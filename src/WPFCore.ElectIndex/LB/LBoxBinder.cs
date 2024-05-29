using System.Windows;
using System.Windows.Input;
using WPFCore.ElectIndex.TV;
using WPFCore.Shared.UI.LB;
using NT = WPFCore.ElectIndex.TV.TIndexNodeEnum;

namespace WPFCore.ElectIndex.LB
{
    public class LBoxBinder : ListBoxBinder
    {
        protected override void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            base.OnDoubleClick(sender, e);
            this.RaiseViewDetailEvent();
        }

        protected LBoxVM VM2 => (LBoxVM)this.VM!; 

        #region Config Commands

        protected void RaiseViewDetailEvent()
        {
            if (!this.IsCommandCanExecute(TACommands.ViewDetailMsg))
                return;

            var lst = this.VM!.SelectedItems.Cast<LBoxItemVM>()
                .Select(i => i.Data)
                .ToList();

            if (lst.First() is TNodeData n)
            {
                var d = new TNodeData
                {
                    NodeType = n.NodeType,
                };
                this.VM2.SendMessage(d);
            }
        }

        // command handler for Refresh
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
