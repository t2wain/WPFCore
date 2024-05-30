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
        private TreeView? _tvw;
        public TreeView? TreeViewControl
        {
            set
            {
                this._tvw = value;
                if (this._tvw != null && !DesignerProperties.GetIsInDesignMode(this._tvw))
                    this.InitTreeView(this._tvw);
            }
            protected get
            {
                return this._tvw;
            }
        }

        // populate from UI DataContext
        protected TreeViewVM? VM { get; set; }

        virtual protected void InitTreeView(TreeView tv)
        {
            // Configure handlers for TreeView events
            tv.SelectedItemChanged += OnSelectedItemChanged;
            tv.MouseDown += this.OnMouseDown;

            // Configure handlers for TreeViewItem events
            tv.AddHandler(TreeViewItem.CollapsedEvent, new RoutedEventHandler(this.OnItemCollapsed));
            tv.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(this.OnItemExpand));
            tv.AddHandler(TreeViewItem.UnselectedEvent, new RoutedEventHandler(this.OnItemUnSelected));
            tv.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(this.OnItemSelected));
            tv.AddHandler(UIElement.MouseRightButtonDownEvent, new RoutedEventHandler(this.OnItemMouseRightButtonDown));
            tv.AddHandler(FrameworkElement.ContextMenuOpeningEvent, new RoutedEventHandler(this.OnContextMenuOpen));
            tv.AddHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(this.OnDoubleClick));
            
            // setup command bindings
            // to listen to routed command
            this.ConfigCommands(tv);
            
            if (tv.DataContext is TreeViewVM vm)
            {
                VM = vm;
            }
        }

        #region TreeView control event handler

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
                this.VM.CollapseAll();
        }

        virtual protected void OnCollapseAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.CollapseAllMsg);
        }

        // command handler Expand All
        virtual protected void OnExpandAll(object sender, RoutedEventArgs e)
        {
            if (this.VM != null)
                this.VM.ExpandAll();
        }

        virtual protected void OnExpandAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.ExpandAllMsg);
        }

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            if (this.VM != null)
                this.VM.Refresh();
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
            if (this._tvw != null)
            {
                var tv = this._tvw;
                tv.SelectedItemChanged -= OnSelectedItemChanged;
                tv.MouseDown -= this.OnMouseDown;

                // Configure handlers for TreeViewItem events
                tv.RemoveHandler(TreeViewItem.CollapsedEvent, new RoutedEventHandler(this.OnItemCollapsed));
                tv.RemoveHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(this.OnItemExpand));
                tv.RemoveHandler(TreeViewItem.UnselectedEvent, new RoutedEventHandler(this.OnItemUnSelected));
                tv.RemoveHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(this.OnItemSelected));
                tv.RemoveHandler(UIElement.MouseRightButtonDownEvent, new RoutedEventHandler(this.OnItemMouseRightButtonDown));
                tv.RemoveHandler(FrameworkElement.ContextMenuOpeningEvent, new RoutedEventHandler(this.OnContextMenuOpen));
                tv.RemoveHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(this.OnDoubleClick));

                tv.CommandBindings.Clear();
                this._tvw = null;
            }

        }
    }
}
