using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LV
{
    public class ListViewBinder
    {
        public ListView ListViewControl { get; protected set; } = null!;

        public ListViewVM VM { get; protected set; } = null!;

        virtual public void InitListView(ListView lv, ListViewVM vm)
        {
            this.ListViewControl = lv;

            // Configure handlers for ListView events
            lv.AddHandler(ListView.MouseDownEvent, new RoutedEventHandler(this.OnMouseDown));
            lv.AddHandler(ListView.MouseRightButtonDownEvent, new RoutedEventHandler(this.OnItemMouseRightButtonDown));
            lv.AddHandler(ListView.ContextMenuOpeningEvent, new RoutedEventHandler(this.OnContextMenuOpen));
            lv.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(this.OnColumnHeaderClick));

            // Configure handlers for TreeViewItem events
            lv.AddHandler(ListViewItem.UnselectedEvent, new RoutedEventHandler(this.OnItemUnSelected));
            lv.AddHandler(ListViewItem.SelectedEvent, new RoutedEventHandler(this.OnItemSelected));

            this.ConfigCommands();

            this.VM = vm;
            lv.DataContext = vm;
            vm.PropertyChanged += this.ListenPropertyChangedOnVM;

        }

        #region ListView control event handler

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
        virtual protected void ConfigCommands()
        {
            foreach (var cb in this.GetCommandBindings())
                this.ListViewControl.CommandBindings.Add(cb);
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
    }
}
