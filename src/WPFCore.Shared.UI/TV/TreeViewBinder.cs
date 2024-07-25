using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFCore.Shared.UI.TV
{
    /// <summary>
    /// Setup event handlers and command bindings for the TreeView control
    /// and retrieve the view model from the DataContext.
    /// </summary>
    public class TreeViewBinder : IDisposable
    {
        public TreeView TreeViewControl { get; protected set; } = null!;

        // populate from UI DataContext
        protected TreeViewVM VM { get; set; } = null!;

        virtual public void InitTreeView(TreeView tv, TreeViewVM vm)
        {
            this.TreeViewControl = tv;
            this.ConfigureEventHandler(tv);
            
            // setup command bindings
            // to listen to routed command
            this.ConfigCommands(tv);

            this.TreeViewControl.DataContext = vm;
            VM = vm;

        }

        #region TreeView control event handler

        RoutedEventHandler _h1 = null!;
        RoutedEventHandler _h2 = null!;
        RoutedEventHandler _h3 = null!;
        RoutedEventHandler _h4 = null!;
        RoutedEventHandler _h5 = null!;
        RoutedEventHandler _h6 = null!;
        RoutedEventHandler _h7 = null!;

        protected void ConfigureEventHandler(TreeView tv)
        {
            // Configure handlers for TreeView events
            tv.SelectedItemChanged += OnSelectedItemChanged;
            tv.MouseDown += this.OnMouseDown;

            // Configure handlers for TreeViewItem events
            _h1 = new RoutedEventHandler(this.OnItemCollapsed);
            tv.AddHandler(TreeViewItem.CollapsedEvent, _h1);

            _h2 = new RoutedEventHandler(this.OnItemExpand);
            tv.AddHandler(TreeViewItem.ExpandedEvent, _h2);

            _h3 = new RoutedEventHandler(this.OnItemUnSelected);
            tv.AddHandler(TreeViewItem.UnselectedEvent, _h3);

            _h4 = new RoutedEventHandler(this.OnItemSelected);
            tv.AddHandler(TreeViewItem.SelectedEvent, _h4);

            _h5 = new RoutedEventHandler(this.OnItemMouseRightButtonDown);
            tv.AddHandler(UIElement.MouseRightButtonDownEvent, _h5);

            _h6 = new RoutedEventHandler(this.OnContextMenuOpen);
            tv.AddHandler(FrameworkElement.ContextMenuOpeningEvent, _h6);

            _h7 = new RoutedEventHandler(this.OnDoubleClick);
            tv.AddHandler(Control.MouseDoubleClickEvent, _h7);

        }

        virtual protected void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) { }

        virtual protected void OnItemCollapsed(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemExpand(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemUnSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnMouseDown(object sender, MouseButtonEventArgs e) { }

        virtual protected void OnItemSelected(object sender, RoutedEventArgs e) 
        {
            if (this.TreeViewControl?.SelectedItem is TreeNodeVM node && this.VM != null)
            {
                this.VM.SelectedItem = node;
            }
        }

        virtual protected void OnItemMouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            var dp = e.OriginalSource as DependencyObject;
            if (dp != null)
            {
                var ti = Utility.FindParent<TreeViewItem>(dp);
                if (ti != null)
                    ti.IsSelected = true;
            }
        }

        virtual protected void OnContextMenuOpen(object sender, RoutedEventArgs e)
        {
            var ti = this.TreeViewControl!.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
            {
                e.Handled = !this.VM.IsContextMenuAllow(ti);
            }
        }

        virtual protected void OnDoubleClick(object sender, RoutedEventArgs e) { }

        #endregion

        #region Config Custom Commands

        // command handler Collapse All
        virtual protected void OnCollapseAll(object sender, RoutedEventArgs e)
        {
            if (this.VM != null)
            {
                e.Handled = true;
                this.VM.CollapseAll();
            }
        }

        virtual protected void OnCollapseAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.CollapseAllMsg);
        }

        // command handler Expand All
        virtual protected void OnExpandAll(object sender, RoutedEventArgs e)
        {
            if (this.VM != null)
            {
                e.Handled = true;
                this.VM.ExpandAll();
            }
        }

        virtual protected void OnExpandAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.ExpandAllMsg);
        }

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            if (this.VM != null)
            {
                e.Handled = true;
                this.VM.Refresh();
            }
        }

        virtual protected void OnRefreshCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.RefreshMsg);
        }

        // Configure the handler for the commands
        virtual protected void ConfigCommands(TreeView tv)
        {
            foreach (var cb in this.GetCommandBindings())
                tv.CommandBindings.Add(cb);
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.CollapseAll, this.OnCollapseAll, this.OnCollapseAllCanExecuted),
                new CommandBinding(TNCommands.ExpandAll, this.OnExpandAll, this.OnExpandAllCanExecuted),
                new CommandBinding(TNCommands.Refresh, this.OnRefresh, this.OnRefreshCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allowed = false;
            var ti = this.TreeViewControl?.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
                allowed = this.VM.IsCommandCanExecute(cmdName, ti);
            return allowed;
        }

        #endregion

        public void Dispose()
        {
            var tv = this.TreeViewControl;
            if (tv != null)
            {
                tv.SelectedItemChanged -= OnSelectedItemChanged;
                tv.MouseDown -= this.OnMouseDown;

                // Configure handlers for TreeViewItem events
                tv.RemoveHandler(TreeViewItem.CollapsedEvent, _h1);
                tv.RemoveHandler(TreeViewItem.ExpandedEvent, _h2);
                tv.RemoveHandler(TreeViewItem.UnselectedEvent, _h3);
                tv.RemoveHandler(TreeViewItem.SelectedEvent, _h4);
                tv.RemoveHandler(UIElement.MouseRightButtonDownEvent, _h5);
                tv.RemoveHandler(FrameworkElement.ContextMenuOpeningEvent, _h6);
                tv.RemoveHandler(Control.MouseDoubleClickEvent, _h7);

                tv.CommandBindings.Clear();
                this.TreeViewControl = null;
            }

        }
    }
}
