using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Controls;
using WPFCore.Common.Data;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.LV;
using WPFCore.Shared.UI;
using WPFCore.Shared.UI.DG;

namespace WPFCore.ElectGrid.DG
{
    public partial class DGridVM : DataGridVM
    {
        IReportDS _ds = null!;

        public DGridVM(IReportDS ds)
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
        private IEnumerable<DataGridColumn> _columns = [];

        public IReportDS ReportDS => this._ds;

        public async Task ShowReport(string reportId)
        {
            var reportDef = await this.ReportDS.GetReportDefinition(reportId);
            reportDef.FileName = reportId;
            await this.ShowReport(reportDef);
        }

        public Task ShowReport(ReportDefinition reportDef)
        {

            this.GridData = null;
            ReportDef = reportDef;
            this.Columns = DataGridConfig.CreateGeneralReport(reportDef);
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
                case nameof(GridData):
                    this.ItemCount = this.GridData?.Count ?? 0;
                    break;
            }
        }

        protected override async Task PopulateData()
        {
            Utility.SetWaitCursor();
            try
            {
                switch (this.ViewType)
                {
                    case LViewEnum.ReportDef:
                        if (this.ReportDef != null)
                        {
                            this.GridData = await this._ds.GetReportData(this.ReportDef);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Name = ex.ToString();
            }
            finally { Utility.SetNormalCursor(); }
        }

        public void UpdateReportDef(ReportDefinition reportDef)
        {
            this.ReportDef!.SetData(reportDef);
            this.Columns = DataGridConfig.CreateGeneralReport(reportDef);
        }

    }
}
