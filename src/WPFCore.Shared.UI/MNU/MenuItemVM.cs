using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
{
    public partial class MenuItemVM : ObservableObject
    {
        public string Name { get; set; } = null!;
        public MenuTypeEnum MenuType { get; set; }
        public MenuItemRole Role { get; set; }

        public bool HasChildren { get { return this.Children.Count > 0; } }

        public bool IsChildrenNotPopulated {
            get
            {
                if (this.HasChildren && this.Children.First() is MenuItemVM vm && vm.MenuType == MenuTypeEnum.Dummy)
                    return true;
                else return false;
            }
        }

        public ObservableCollection<INotifyPropertyChanged> Children { get; init; } = [];

        public void Poplulate(IEnumerable<INotifyPropertyChanged> nodes)
        {
            this.Children.Clear();
            this.AddChildrenNodes(nodes);
        }

        protected void AddChildrenNodes(IEnumerable<INotifyPropertyChanged> nodes) =>
            nodes.Aggregate(this.Children,
                (agg, child) => { agg.Add(child); return agg; });


    }
}
