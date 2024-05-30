using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFCore.Shared.UI.TV
{
    public partial class TreeNodeVM : ObservableObject
    {
        public TreeNodeVM()
        {
            this.Children = new ObservableCollection<INotifyPropertyChanged>();
        }

        public string ID { get; set; } = null!;

        [ObservableProperty]
        private string name = null!;

        public bool IsLeafNode { get; set; }

        [ObservableProperty]
        private bool isExpanded = false;

        public bool HasChildren { get { return this.Children.Count > 0; } }

        public bool IsChildrenNotPopulated =>
            this.HasChildren && this.Children.First() is DummyNodeVM;

        public TreeViewVM Parent { get; set; } = null!;

        public ObservableCollection<RoutedUICommand> TreeViewContextMenu => 
            this.Parent.TreeViewContextMenu;

        [ObservableProperty]
        private ObservableCollection<INotifyPropertyChanged> children;

        [ObservableProperty]
        private ICollectionView childrenView = null!;

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(this.Children):
                    this.ChildrenView = new ListCollectionView(this.Children);
                    break;
            }
        }

        virtual public void PopulateData() { }

        virtual public void RefreshData() { this.PopulateData(); }

        protected void AddChildrenNodes(IEnumerable<INotifyPropertyChanged> nodes) =>
            nodes.Aggregate(this.Children,
                (agg, child) => { agg.Add(child); return agg; });

    }
}
