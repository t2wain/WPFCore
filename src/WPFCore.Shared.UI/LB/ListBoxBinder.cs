using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LB
{
    /// <summary>
    /// Setup event handlers and command bindings for the ListBox control
    /// and retrieve the view model from the DataContext.
    /// </summary>
    public class ListBoxBinder : IDisposable
    {
        public ListBox ListBoxControl { get; protected set; } = null!;

        protected ListBoxVM VM { get; set; } = null!;

        RoutedEventHandler _h1 = null!;

        virtual public void InitListView(ListBox lv, ListBoxVM vm)
        {
            this.ListBoxControl = lv;

            _h1 = new RoutedEventHandler(this.OnDoubleClick);
            lv.AddHandler(Control.MouseDoubleClickEvent, _h1);
            this.ConfigCommands(lv);

            this.VM = vm;
            this.ListBoxControl.DataContext = vm;
            vm.PropertyChanged += this.ListenPropertyChangedOnVM;

        }

        public ListBoxItemVM? SelectedItem =>
            this.ListBoxControl.SelectedItem as ListBoxItemVM;

        virtual protected void OnDoubleClick(object sender, RoutedEventArgs e) { }

        #region Config Commands

        // command handler Collapse All
        virtual protected void OnSelectAll(object sender, RoutedEventArgs e)
        {
            this.VM?.SelectAllItems();
        }

        virtual protected void OnSelectAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.SelectAllMsg);
        }

        // command handler Collapse All
        virtual protected void OnUnSelectAll(object sender, RoutedEventArgs e)
        {
            this.VM?.UnSelectAllItems();
        }

        virtual protected void OnUnSelectAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.UnselectAllMsg);
        }

        // Configure the handler for the commands
        virtual protected void ConfigCommands(ListBox lv)
        {
            foreach (var cb in this.GetCommandBindings())
                lv.CommandBindings.Add(cb);
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.SelectAll, this.OnSelectAll, this.OnSelectAllCanExecuted),
                new CommandBinding(TNCommands.UnselectAll, this.OnUnSelectAll, this.OnUnSelectAllCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allow = false;
            switch (cmdName)
            {
                case TNCommands.SelectAllMsg:
                case TNCommands.UnselectAllMsg:
                    if (this.VM != null)
                        allow = this.VM.ListItems.Count > 0;
                    break;
            }
            return allow;
        }

        #endregion

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }

        public void Dispose()
        {
            var lvw = this.ListBoxControl;
            if (lvw != null)
            {
                lvw.RemoveHandler(Control.MouseDoubleClickEvent, _h1);
                lvw.CommandBindings.Clear();
                if (this.VM != null)
                    this.VM.PropertyChanged -= this.ListenPropertyChangedOnVM;
                this.ListBoxControl = null!;
            }
        }
    }
}
