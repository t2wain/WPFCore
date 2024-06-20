using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Data;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LV
{
    public partial class ListViewVM : ObservableRecipient
    {

        protected void Init()
        {
            this.Root = new ObservableCollection<INotifyPropertyChanged>();
            this.ListViewContextMenu = new ObservableCollection<RoutedUICommand>(this.GetContextCommands());
        }

        #region Properties

        [ObservableProperty]
        private bool _isEnabled = true;

        #region Data

        // Data can be either a DataView (ListData)
        // or a collection of objects (Root)

        [ObservableProperty]
        private DataView? _listData;

        [ObservableProperty]
        private ObservableCollection<INotifyPropertyChanged>? _root;

        [ObservableProperty]
        private ICollectionView? _listItemsView;

        virtual public Task RefreshData()
        {
            this.ListData?.Table?.Clear();
            return this.PopulateData();
        }

        virtual protected Task PopulateData() => Task.CompletedTask;

        #endregion

        public void RaisePropertyChangeEvent(string propertyName)
        {
            this.OnPropertyChanged(propertyName);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Root):
                    this.ListItemsView = CollectionViewSource.GetDefaultView(this.Root);
                    break;
                case nameof(ListData):
                    this.ListItemsView = CollectionViewSource.GetDefaultView(this.ListData);
                    break;
            }
        }

        #endregion

        #region Sorting

        public void SortColumn(string propertyName, bool isMultiColumn)
        {
            if (this.ListItemsView != null)
            {
                var v = new ListViewSort(this.ListItemsView);
                v.SetSort(propertyName, isMultiColumn);
            }
        }

        #endregion

        #region Context Menu

        public ObservableCollection<RoutedUICommand> ListViewContextMenu { get; set; } = null!;

        virtual protected List<RoutedUICommand> GetContextCommands()
        {
            return new List<RoutedUICommand>
            {
                TNCommands.SelectAll, 
                TNCommands.UnselectAll, 
            };
        }

        // determine which item allow to show context menu
        virtual public bool IsContextMenuAllow(INotifyPropertyChanged di)
        {
            this.EnableContextMenu(di);
            return this.ListViewContextMenu.Count > 0;
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
