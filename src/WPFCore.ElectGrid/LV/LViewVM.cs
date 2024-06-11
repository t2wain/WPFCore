using CommunityToolkit.Mvvm.ComponentModel;
using WPFCore.Data.OleDb;
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

        public async Task ShowReport(string? reportId)
        {
            if (reportId != null)
            {
                this.ListData = null;
                ReportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
                await this.RefreshData();
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
                        var db = this._ds.NewDB();
                        this.ListData = await ReportUtil.LoadReport(db, this.ReportDef);
                        db.Dispose();
                    }
                    break;
            }
            Utility.SetNormalCursor();
        }
    }
}
