using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.DG;
using WPFCore.ElectGrid.LV;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.TC
{
    public class ReportTabItem : TabItem, IDisposable
    {
        public LViewVM VM { get; set; } = null!;
        public DGridVM DGVM { get; set; } = null!;

        public string ID { get; set; } = null!;

        public async Task ShowReport(string reportId, TabControl utc, IServiceProvider provider)
        {
            var reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            
            //switch (reportDef.DatabaseObjectType)
            //{
            //    case ReportDefinition.DB_TYPE_VIEW:
            //    case ReportDefinition.DB_TYPE_TABLE:
            //    case ReportDefinition.DB_TYPE_PROC:
            //        ShowListViewReport(reportId, utc, provider, reportDef);
            //        break;
            //    case ReportDefinition.DB_TYPE_TABLE_EDIT:
            //    case ReportDefinition.DB_TYPE_PROC_EDIT:
            //        ShowDataGridReport(reportId, utc, provider, reportDef);
            //        break;
            //}
            ShowDataGridReport(reportId, utc, provider, reportDef);

        }

        public async Task ShowListViewReport(string reportId, TabControl utc, 
            IServiceProvider provider, ReportDefinition? rdef = null)
        {
            var lvm = provider.GetRequiredService<LViewVM>();
            this.VM = lvm;
            this.ID = reportId;
            var ru = new UListView();
            ru.Init(lvm);

            var reportDef = rdef;
            if (reportDef == null)
                reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);
            this.Header = reportDef.Name;
            this.Content = ru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await lvm.ShowReport(reportDef);
        }

        public async Task ShowDataGridReport(string reportId, TabControl utc, 
            IServiceProvider provider, ReportDefinition? rdef = null)
        {

            var reportDef = rdef;
            if (reportDef == null)
                reportDef = await ReportUtil.DeserializeReportDefinitionFromFile(reportId);

            var dgvm = provider.GetRequiredService<DGridVM>();
            this.DGVM = dgvm;
            this.ID = reportId;
            var ru = new UDataGridView();
            ru.Init(dgvm);

            this.Header = reportDef.Name;
            this.Content = ru;
            utc.Items.Add(this);
            this.IsSelected = true;

            await dgvm.ShowReport(reportDef);
        }

        public void Dispose()
        {
        }
    }
}
