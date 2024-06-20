using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Controls;
using WPFCore.Common.Data;
using WPFCore.Data.Report;
using WPFCore.Shared.UI;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public partial class LViewVM : ListViewVM
    {

        IReportDS _ds = null!;

        public LViewVM(IReportDS ds)
        {
            this._ds = ds;
            this.Init();
        }

        public LViewEnum ViewType { get; set; }

        [ObservableProperty]
        string? _name;

        [ObservableProperty]
        private ReportDefinition? _reportDef;

        [ObservableProperty]
        private int _itemCount;

        [ObservableProperty]
        private ViewBase? _gridView;

        public async Task ShowReport(string reportId)
        {
            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            await this.ShowReport(reportDef);
        }

        public Task ShowReport(ReportDefinition reportDef)
        {

            this.ListData = null;
            ReportDef = reportDef;
            this.GridView = GridConfig.CreateGeneralReport(reportDef);
            this.Name = reportDef.Name;
            return this.PopulateData();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Root):
                    this.ItemCount = this.Root?.Count() ?? 0;
                    break;
                case nameof(ListData):
                    this.ItemCount = this.ListData?.Count ?? 0;
                    break;
            }
        }

        protected override async Task PopulateData()
        {
            Utility.SetWaitCursor();
            switch (this.ViewType)
            {
                case LViewEnum.ReportDef:
                    if (this.ReportDef != null)
                    {
                        this.ListData = await this._ds.GetReportData(this.ReportDef);
                    }
                    break;
            }
            Utility.SetNormalCursor();
        }
    }
}
