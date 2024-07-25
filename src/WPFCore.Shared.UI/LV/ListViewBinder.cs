using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LV
{
    public class ListViewBinder : IDisposable
    {
        public ListView ListViewControl { get; protected set; } = null!;

        public ListViewVM VM { get; protected set; } = null!;

        virtual public void InitListView(ListView lv, ListViewVM vm)
        {
            this.ListViewControl = lv;

            this.ConfigureEvent(lv, vm);

            this.ConfigCommands();

            this.VM = vm;
            lv.DataContext = vm;
        }

        #region ListView control event handler

        RoutedEventHandler _h1 = null!;
        RoutedEventHandler _h2 = null!;
        RoutedEventHandler _h3 = null!;
        RoutedEventHandler _h4 = null!;
        RoutedEventHandler _h5 = null!;
        RoutedEventHandler _h6 = null!;

        void ConfigureEvent(ListView lv, ListViewVM vm)
        {
            // Configure handlers for ListView events
            _h1 = new RoutedEventHandler(this.OnMouseDown);
            lv.AddHandler(ListView.MouseDownEvent, _h1);

            _h2 = new RoutedEventHandler(this.OnItemMouseRightButtonDown);
            lv.AddHandler(ListView.MouseRightButtonDownEvent, _h2);

            _h3 = new RoutedEventHandler(this.OnContextMenuOpen);
            lv.AddHandler(ListView.ContextMenuOpeningEvent, _h3);

            _h4 = new RoutedEventHandler(new RoutedEventHandler(this.OnColumnHeaderClick));
            lv.AddHandler(GridViewColumnHeader.ClickEvent, _h4);

            // Configure handlers for TreeViewItem events
            _h5 = new RoutedEventHandler(new RoutedEventHandler(this.OnItemUnSelected));
            lv.AddHandler(ListViewItem.UnselectedEvent, _h5);

            _h6 = new RoutedEventHandler(new RoutedEventHandler(this.OnItemSelected));
            lv.AddHandler(ListViewItem.SelectedEvent, _h6);

            vm.PropertyChanged += this.ListenPropertyChangedOnVM;
        }

        virtual protected void OnItemUnSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnMouseDown(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemMouseRightButtonDown(object sender, RoutedEventArgs e)
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
            var ti = this.ListViewControl?.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
            {
                e.Handled = !this.VM.IsContextMenuAllow(ti);
            }
        }

        virtual protected void OnColumnHeaderClick(object sender, RoutedEventArgs e) 
        {
            if (e.OriginalSource is GridViewColumnHeader col)
            {
                // allow column sortings
                bool isMultiColumn = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
                this.SortColumn(col.Name, isMultiColumn);
            }
        }

        #endregion

        #region Config Commands

        // Configure the handler for the commands
        void ConfigCommands()
        {
            foreach (var cb in this.GetCommandBindings())
                this.ListViewControl.CommandBindings.Add(cb);
        }

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
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
            var ti = this.ListViewControl.SelectedItem as INotifyPropertyChanged;
            if (ti != null)
                allowed = this.VM.IsCommandCanExecute(cmdName, ti);
            return allowed;
        }

        #endregion

        virtual protected void SortColumn(string propertyName, bool isMultiColumn = false) 
        {
            this.VM.SortColumn(propertyName, isMultiColumn);
        }

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }

        public virtual void Dispose()
        {
            var lv = this.ListViewControl;
            lv.RemoveHandler(ListView.MouseDownEvent, _h1);
            lv.RemoveHandler(ListView.MouseRightButtonDownEvent, _h2);
            lv.RemoveHandler(ListView.ContextMenuOpeningEvent, _h3);
            lv.RemoveHandler(GridViewColumnHeader.ClickEvent, _h4);
            lv.RemoveHandler(ListViewItem.UnselectedEvent, _h5);
            lv.RemoveHandler(ListViewItem.SelectedEvent, _h6);

            var vm = this.VM;
            vm.PropertyChanged -= this.ListenPropertyChangedOnVM;

            this.ListViewControl = null!;
        }
    }
}
