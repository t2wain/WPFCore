using System.Data;
using System.Windows.Controls;
using WPFCore.Data.Report;

namespace WPFCore.ElectGrid.LV
{
    public record ReportResult
    {
        public ReportDefinition ReportDef { get; set; } = null!;
        public DataView ListData { get; set; } = null!;
        public ViewBase GridView { get; set; } = null!;
    }
}
