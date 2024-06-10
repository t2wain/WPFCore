using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Data;
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

        public ReportDefinition? ReportDef { get; set; }

        protected override async Task PopulateData()
        {
            Utility.SetWaitCursor();
            switch (this.ViewType)
            {
                case LViewEnum.ReportDef:
                    if (this.ReportDef != null)
                        this.ListData = await ReportUtil.LoadReport(this._ds.DB, this.ReportDef);
                    break;
            }
            Utility.SetNormalCursor();
        }
    }
}
