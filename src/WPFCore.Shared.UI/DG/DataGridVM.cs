using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Data;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.DG
{
    public partial class DataGridVM : ObservableRecipient
    {
        protected void Init()
        {
            this.Root = new ObservableCollection<INotifyPropertyChanged>();
            this.GridContextMenu = new ObservableCollection<RoutedUICommand>(this.GetContextCommands());
        }

        #region Properties

        [ObservableProperty]
        private bool _isEnabled = true;

        #region Data

        // Data can be either a DataView (ListData)
        // or a collection of objects (Root)

        [ObservableProperty]
        private DataView? _gridData;

        [ObservableProperty]
        private ObservableCollection<INotifyPropertyChanged>? _root;

        [ObservableProperty]
        private ICollectionView? _gridItemsView;

        virtual public Task RefreshData()
        {
            this.GridData?.Table?.Clear();
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
                    this.GridItemsView = CollectionViewSource.GetDefaultView(this.Root);
                    break;
                case nameof(GridData):
                    this.GridItemsView = CollectionViewSource.GetDefaultView(this.GridData);
                    break;
            }
        }

        #endregion

        #region Context Menu

        public ObservableCollection<RoutedUICommand> GridContextMenu { get; set; } = null!;

        virtual protected List<RoutedUICommand> GetContextCommands()
        {
            return new List<RoutedUICommand>
            {
                TNCommands.Refresh,
                TNCommands.SelectAll,
                TNCommands.UnselectAll,
                ApplicationCommands.Paste,
            };
        }

        // determine which item allow to show context menu
        virtual public bool IsContextMenuAllow(INotifyPropertyChanged? di)
        {
            this.EnableContextMenu(di);
            return this.GridContextMenu.Count > 0;
        }

        // determine which menu is available for the item
        virtual protected void EnableContextMenu(INotifyPropertyChanged? di)  {  }

        // determine which menu can be executed for the item
        virtual public bool IsCommandCanExecute(string cmdName, INotifyPropertyChanged? di)
        {
            var allowed = cmdName switch
            {
                TNCommands.RefreshMsg => true,
                TNCommands.SelectAllMsg or TNCommands.UnselectAllMsg => !this.GridItemsView!.IsEmpty,
                _ => false
            };
            return allowed;
        }

        #endregion
    }
}
