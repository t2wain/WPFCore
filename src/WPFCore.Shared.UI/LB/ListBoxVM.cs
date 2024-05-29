using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LB
{
    public partial class ListBoxVM : ObservableRecipient
    {
        virtual protected void Init()
        {
            this.ListItems = new ObservableCollection<INotifyPropertyChanged>();
            this.ListBoxContextMenu = new ObservableCollection<RoutedUICommand>(this.GetContextCommands());
        }

        [ObservableProperty]
        private ObservableCollection<INotifyPropertyChanged> _listItems = null!;

        [ObservableProperty]
        private ICollectionView? _listItemsView;

        public ObservableCollection<RoutedUICommand> ListBoxContextMenu { get; set; } = null!;

        public IEnumerable<INotifyPropertyChanged> SelectedItems =>
            this.ListItems.Cast<ListBoxItemVM>().Where(i => i.IsSelected).ToList();

        virtual public void SelectAllItems() 
        {
            if (this.ListItemsView != null)
            {
                foreach (var it in this.ListItemsView)
                {
                    if (it is ListBoxItemVM li)
                        li.IsSelected = true;
                }
            }
        }

        virtual public void UnSelectAllItems() 
        {
            if (this.ListItemsView != null)
            {
                foreach (ListBoxItemVM it in this.ListItemsView)
                {
                    if (it is ListBoxItemVM li)
                        li.IsSelected = false;
                }
            }
        }

        virtual public void RefreshData() { }

        virtual public void PopulateData() { }

        virtual public void PopulateData(object data) { }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(ListItems):
                    this.ListItemsView = new ListCollectionView(this.ListItems);
                    break;
            }
        }

        protected void AddItems(IEnumerable<INotifyPropertyChanged> items)
        {
            items.Aggregate(this.ListItems, (agg, i) => { agg.Add(i); return agg; });
        }

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

        #endregion
    }
}
