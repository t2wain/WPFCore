using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Data;
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
        private ReportDefinition? _reportDef;

        [ObservableProperty]
        private int _itemCount;

        public async Task ShowReport(string? reportId)
        {
            if (reportId != null)
            {
                this.ListData = null;
                ReportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
                await this.PopulateData();
            }

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
