using CommunityToolkit.Mvvm.ComponentModel;
using WPFCore.Data.Report;
using WPFCore.Shared.UI.DLG;

namespace WPFCore.ElectGrid.RPT
{
    public partial class UReportDefVM : DialogVM
    {
        public UReportDefVM() : base() { }

        [ObservableProperty]
        private ReportDefinition? _reportDef;
    }
}
