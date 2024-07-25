using WPFCore.Data.Report;

namespace WPFCore.ElectGrid.TC
{
    public interface IReportTabItemContent
    {
        Task ShowReport(ReportDefinition rdef, IServiceProvider provider);
        void ShowEditWindow();
        Task RefreshData();
        void SetFilter();
        void ClearFilter();
        int ItemCount { get; }
    }
}
