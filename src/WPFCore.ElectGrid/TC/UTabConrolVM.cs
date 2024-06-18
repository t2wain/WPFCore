using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.LV;

namespace WPFCore.ElectGrid.TC
{
    public partial class UTabConrolVM : ObservableObject
    {
        private readonly IServiceProvider _provider;

        public UTabConrolVM(IServiceProvider provider)
        {
            this._provider = provider;
            this.LVM = this._provider.GetRequiredService<LViewVM>();
        }

        [ObservableProperty]
        ObservableCollection<INotifyPropertyChanged> _items = [];

        [ObservableProperty]
        INotifyPropertyChanged? _selectedItem;

        public LViewVM LVM { get; set; }

        public async Task AddReport(string reportId) 
        {
            var tvm = new TabItemVM();
            tvm.ItemType = TabItemEnum.Report;
            tvm.IsSelected = true;
            tvm.ItemID = reportId;

            tvm.TabDataContext = this.LVM;
            this.Items.Add(tvm);
            this.SelectedItem = tvm;
            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            tvm.Name = reportDef.Name;
            var ctx = await this.LVM.ShowReport(reportDef);
            tvm.Report = ctx;
        }
    }
}
