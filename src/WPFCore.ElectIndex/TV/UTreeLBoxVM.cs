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
        // These are faux properties. The intention is to use
        // the PropertyChanged notification to communicate an event.
        public const string ExecuteViewDetailCmdTVEvent = "ExecuteViewDetailCmdTVEvent";
        public const string ExecuteViewDetailCmdLBEvent = "ExecuteViewDetailCmdLBEvent";

        [ObservableProperty]
        private int _itemCount = 0;

        public UTreeLBoxVM(TreeVM tvm, LBoxVM lvm)
        {
            this.TreeVM = tvm;
            this.LBoxVM = lvm;
            this.TreeVM.PropertyChanged += OnTVPropertyChanged;
            this.LBoxVM.PropertyChanged += OnLBPropertyChanged;
        }

        private void OnLBPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(LBoxVM.ItemCount):
                    this.ItemCount = this.LBoxVM.ItemCount;
                    break;
                case LBoxVM.ExecuteViewDetailCmdEvent:
                    this.OnPropertyChanged(UTreeLBoxVM.ExecuteViewDetailCmdLBEvent);
                    break;
            }
        }

        /// <summary>
        /// Linking both TreeView and Listbox view models
        /// </summary>
        private void OnTVPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.TreeVM.SelectedItem):
                case TreeVM.SelectedItemChildrenRefreshedEvent:
                    if (this.TreeVM.SelectedItem is INotifyPropertyChanged n)
                    {
                        this.LBoxVM.CurrentIndexNode = n;
                        this.LBoxVM.PopulateData();
                    }
                    break;
                case TreeVM.ExecuteViewDetailCmdEvent:
                    this.OnPropertyChanged(UTreeLBoxVM.ExecuteViewDetailCmdTVEvent);
                    break;
            }
        }

        [ObservableProperty]
        private TreeVM _treeVM = null!;

        [ObservableProperty]
        private LBoxVM _lBoxVM = null!;

    }
}
