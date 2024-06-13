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
        public const string ExecuteViewDetailCmdTVEvent = "WPFCore.ElectIndex.TV.UTreeLBoxVM.ExecuteViewDetailCmdTVEvent";
        public const string ExecuteViewDetailCmdLBEvent = "WPFCore.ElectIndex.TV.UTreeLBoxVM.ExecuteViewDetailCmdLBEvent";

        [ObservableProperty]
        private int _itemCount = 0;

        public TreeVM TreeVM { get; protected set; }
        public LBoxVM LBoxVM { get; protected set; }

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
                    this.RaisePropertyChangeEvent(UTreeLBoxVM.ExecuteViewDetailCmdLBEvent);
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
                    this.RaisePropertyChangeEvent(UTreeLBoxVM.ExecuteViewDetailCmdTVEvent);
                    break;
            }
        }

        virtual public void RaisePropertyChangeEvent(string eventId)
        {
            this.OnPropertyChanged(eventId);
        }

    }
}
