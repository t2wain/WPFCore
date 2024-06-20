using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.LV;

namespace WPFCore.ElectGrid.TC
{
    public class ReportTabItem : TabItem
    {
        public LViewVM VM { get; set; } = null!;

        public async Task ShowReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var lvm = provider.GetRequiredService<LViewVM>();
            this.VM = lvm;
            var ru = new UListView();
            ru.Init(lvm);

            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            this.Header = reportDef.Name;
            this.Content = ru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await lvm.ShowReport(reportDef);
        }
    }
}
