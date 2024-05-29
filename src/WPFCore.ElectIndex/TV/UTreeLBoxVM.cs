using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using WPFCore.ElectIndex.LB;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// View model for control UTreeView
    /// </summary>
    public partial class UTreeLBoxVM : ObservableObject
    {

        public UTreeLBoxVM(TreeVM tvm, LBoxVM lvm)
        {
            this.TreeVM = tvm;
            this.LBoxVM = lvm;
            this.TreeVM.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.TreeVM.SelectedItem):
                case TreeVM.SelectedItemChildrenRefreshed:
                    if (this.TreeVM.SelectedItem is INotifyPropertyChanged n)
                    {
                        this.LBoxVM.CurrentIndexNode = n;
                        this.LBoxVM.PopulateData();
                    }
                    break;
            }
        }

        [ObservableProperty]
        private TreeVM _treeVM = null!;

        [ObservableProperty]
        private LBoxVM _lBoxVM = null!;

    }
}
