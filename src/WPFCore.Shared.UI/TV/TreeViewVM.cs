using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFCore.Shared.UI.TV
{
    public partial class TreeViewVM : ObservableRecipient
    {
        #region Init

        public TreeViewVM()
        {
            this.Init();
        }

        protected void Init()
        {
            this.Root = new ObservableCollection<INotifyPropertyChanged>();
            this.TreeViewContextMenu = new ObservableCollection<RoutedUICommand>(this.GetContextCommands());
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<INotifyPropertyChanged>? _root;

        [ObservableProperty]
        private ICollectionView? _rootView;

        [ObservableProperty]
        private INotifyPropertyChanged? _selectedItem;

        public ObservableCollection<RoutedUICommand> TreeViewContextMenu { get; set; } = null!;

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Root):
                    this.RootView = new ListCollectionView(this.Root);
                    break;
            }
        }

        virtual public void RaisePropertyChangeEvent(string eventId)
        {
            this.OnPropertyChanged(eventId);
        }

        #endregion

        #region Command Action

        virtual public void CollapseAll()
        {
            var ti = this.SelectedItem as TreeNodeVM;
            if (ti != null)
                this.IterateAll(ti,
                    di => di.HasChildren,
                    di => di.IsExpanded = false);
        }

        virtual public void ExpandAll()
        {
            var ti = this.SelectedItem as TreeNodeVM;
            if (ti != null)
                this.IterateAll(ti,
                di => di.HasChildren,
                di => di.IsExpanded = true);
        }

        virtual public void Expand()
        {
            var ti = this.SelectedItem as TreeNodeVM;
            if (ti != null)
                ti.IsExpanded = true;
        }

        virtual public void Collapse()
        {
            var ti = this.SelectedItem as TreeNodeVM;
            if (ti != null)
                ti.IsExpanded = false;
        }

        virtual public void Refresh()
        {
            var n = this.SelectedItem as TreeNodeVM;
            if (n != null)
                n.RefreshData();
        }

        protected void IterateAll(TreeNodeVM dataItem, Predicate<TreeNodeVM> pred, Action<TreeNodeVM> act)
        {
            if (pred(dataItem)) act(dataItem);
            foreach (TreeNodeVM ci in dataItem.ChildrenView)
                IterateAll(ci, pred, act);
        }

        #endregion

        #region Context Menu

        virtual protected List<RoutedUICommand> GetContextCommands()
        {
            return new List<RoutedUICommand>
            {
                TNCommands.Refresh, 
                TNCommands.ExpandAll, 
                TNCommands.CollapseAll, 
            };
        }

        // determine which item allow to show context menu
        virtual public bool IsContextMenuAllow(INotifyPropertyChanged di)
        {
            this.EnableContextMenu(di);
            return this.TreeViewContextMenu.Count > 0;
        }

        // determine which menu is available for the item
        virtual protected void EnableContextMenu(INotifyPropertyChanged di)
        {
            //this.TreeViewContextMenu.Clear();
        }

        // determine which menu can be executed for the item
        virtual public bool IsCommandCanExecute(string cmdName, INotifyPropertyChanged di)
        {
            var allowed = true;
            return allowed;
        }

        #endregion
    }
}
