using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace WPFCore.ElectGrid.LV
{
    public partial class UListViewVM : ObservableObject
    {
        public UListViewVM(LViewVM vm)
        {
            this.ListVM = vm;
            this.Label = "<No Listing>";
        }

        public void Init()
        {
            this.ListVM.PropertyChanged += this.ListenPropertyChangedOnVM;
        }

        [ObservableProperty]
        string _label = "";

        [ObservableProperty]
        LViewVM _listVM = null!;

        protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentIndexNode":
                    this.UpdateLabel();
                    break;
            }
        }

        protected void UpdateLabel()
        {
            var lst = new List<string>();

            var itemName = "";
            switch (this.ListVM.ViewType)
            {
                case LViewEnum.ReportDef:
                    itemName = "IPMS BOMs";
                    break;
            }

            lst.Add(itemName);
            this.Label = String.Join(" :: ", lst.ToArray());
        }

        //protected void OnTreeNodeChange(TNodeData args)
        //{
        //    this.ListVM.CurrentIndexNode = args;
        //    this.ListVM.IsEnabled = false;
        //    this.ListVM.PopulateData();
        //}
    }
}
