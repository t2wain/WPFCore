using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.DG
{
    public class DataGridBinder : IDisposable
    {
        public DataGrid GridControl { get; protected set; } = null!;

        public DataGridVM VM { get; protected set; } = null!;

        virtual public void InitDataGrid(DataGrid dg, DataGridVM vm)
        {
            this.GridControl = dg;

            this.ConfigureEvent(dg, vm);

            this.ConfigCommands();

            this.VM = vm;
            dg.DataContext = vm;
        }

        #region ListView control event handler

        RoutedEventHandler _h1 = null!;
        RoutedEventHandler _h2 = null!;
        RoutedEventHandler _h3 = null!;
        RoutedEventHandler _h5 = null!;
        RoutedEventHandler _h6 = null!;

        void ConfigureEvent(DataGrid dg, DataGridVM vm)
        {
            // Configure handlers for ListView events
            _h1 = new RoutedEventHandler(this.OnGridMouseDown);
            dg.AddHandler(DataGrid.MouseDownEvent, _h1);

            _h2 = new RoutedEventHandler(this.OnGridMouseRightButtonDown);
            dg.AddHandler(DataGrid.MouseRightButtonDownEvent, _h2);

            _h3 = new RoutedEventHandler(this.OnContextMenuOpen);
            dg.AddHandler(DataGrid.ContextMenuOpeningEvent, _h3);

            // Configure handlers for TreeViewItem events
            _h5 = new RoutedEventHandler(new RoutedEventHandler(this.OnRowUnSelected));
            dg.AddHandler(DataGridRow.UnselectedEvent, _h5);

            _h6 = new RoutedEventHandler(new RoutedEventHandler(this.OnRowSelected));
            dg.AddHandler(DataGridRow.SelectedEvent, _h6);

            vm.PropertyChanged += this.ListenPropertyChangedOnVM;
        }

        virtual protected void OnRowUnSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnRowSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnGridMouseDown(object sender, RoutedEventArgs e) { }

        virtual protected void OnGridMouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is DependencyObject dp)
            {
                var ti = Utility.FindParent<ListViewItem>(dp);
                if (ti != null)
                    ti.IsSelected = true;
            }
        }

        virtual protected void OnContextMenuOpen(object sender, RoutedEventArgs e)
        {
            var ti = this.GridControl?.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
            {
                e.Handled = !this.VM.IsContextMenuAllow(ti);
            }
        }

        #endregion

        #region Config Commands

        // Configure the handler for the commands
        void ConfigCommands()
        {
            foreach (var cb in this.GetCommandBindings())
                this.GridControl.CommandBindings.Add(cb);
        }

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            this.VM.RefreshData();
        }

        virtual protected void OnRefreshCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.RefreshMsg);
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.Refresh, this.OnRefresh, this.OnRefreshCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allowed = false;
            var ti = this.GridControl.SelectedItem as INotifyPropertyChanged;
            if (ti != null)
                allowed = this.VM.IsCommandCanExecute(cmdName, ti);
            return allowed;
        }

        #endregion

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }

        public virtual void Dispose()
        {
            var dg = this.GridControl;
            dg.RemoveHandler(DataGrid.MouseDownEvent, _h1);
            dg.RemoveHandler(DataGrid.MouseRightButtonDownEvent, _h2);
            dg.RemoveHandler(DataGrid.ContextMenuOpeningEvent, _h3);
            dg.RemoveHandler(DataGridRow.UnselectedEvent, _h5);
            dg.RemoveHandler(DataGridRow.SelectedEvent, _h6);

            var vm = this.VM;
            vm.PropertyChanged -= this.ListenPropertyChangedOnVM;

            this.GridControl = null!;
        }

    }
}
