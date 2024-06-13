using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;
using WPFCore.ElectIndex.TV;
using WPFCore.Shared.UI.LB;

namespace WPFCore.ElectIndex.LB
{
    /// <summary>
    /// View model for ListBox
    /// </summary>
    public partial class LBoxVM : ListBoxVM
    {
        // These are faux properties. The intention is to use
        // the PropertyChanged notification to communicate an event.
        public const string ExecuteViewDetailCmdEvent = "WPFCore.ElectIndex.LB.LBoxVM.ExecuteViewDetailCmdEvent";

        public LBoxVM()
        {
            this.Init();
        }

        [ObservableProperty]
        INotifyPropertyChanged? _currentIndexNode;

        public override void PopulateData()
        {
            this.ListItems.Clear();
            if (this.CurrentIndexNode is NodeVM n && n.Children.Count > 0)
            {
                var children = n.Children
                    .Where(n => n is NodeVM)
                    .Cast<NodeVM>()
                    .Where(n => n.DataItem?.Data != null)
                    .Select(n => new LBoxItemVM
                    {
                        Name = n.Name,
                        Data = n.DataItem,
                        Parent = this
                    })
                    .ToList();

                this.AddItems(children);
            }
            this.ItemCount = this.ListItems.Count;
        }

        protected override List<RoutedUICommand> GetContextCommands()
        {
            var lst = base.GetContextCommands();
            lst.Add(TACommands.ViewDetail);
            return lst;
        }

        public void SendMessage(TNodeData data) { }
    }
}
