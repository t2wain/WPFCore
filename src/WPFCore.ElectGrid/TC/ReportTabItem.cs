using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.DG;
using WPFCore.ElectGrid.LV;

namespace WPFCore.ElectGrid.TC
{
    public class ReportTabItem : TabItem
    {
        public LViewVM VM { get; set; } = null!;
        public DGridVM DGVM { get; set; } = null!;

        public string ID { get; set; } = null!;

        public async Task ShowListViewReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var lvm = provider.GetRequiredService<LViewVM>();
            this.VM = lvm;
            this.ID = reportId;
            var ru = new UListView();
            ru.Init(lvm);

            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            this.Header = reportDef.Name;
            this.Content = ru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await lvm.ShowReport(reportDef);
        }

        public async Task ShowDataGridReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var dgvm = provider.GetRequiredService<DGridVM>();
            this.DGVM = dgvm;
            this.ID = reportId;
            var ru = new UDataGridView();
            ru.Init(dgvm);

            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            this.Header = reportDef.Name;
            this.Content = ru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await dgvm.ShowReport(reportDef);
        }

    }
}
