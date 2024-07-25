using WPFCore.Common.Data;
using WPFCore.Data.Report;
using WPFCore.Shared.UI.DLG;

namespace WPFCore.ElectGrid.RPT
{
    public static class ReportDialogUtility
    {
        public static bool ShowEditWindow(ref ReportDefinition reportDef, IReportDS ds)
        {
            // Configure dialog window
            var c = new UReportDef();
            var dlvm = new UReportDefVM(ds);
            dlvm.ReportDef = reportDef;
            c.Init(dlvm);

            // Show dialog
            var dlres = DialogUtility.GetDialogWindow(c, $"Report Edit - {reportDef.Name}", null, 470).ShowDialog();

            // Update column defs
            return (dlres.HasValue && dlres.Value);
        }

        public static bool ShowFilterWindow(ref ReportDefinition reportDef)
        {
            // Configure dialog window
            var c = new UReportFilter();
            var dlvm = new UReportDefVM();
            dlvm.ReportDef = reportDef;
            c.Init(dlvm);

            // Show dialog
            var dlres = DialogUtility.GetDialogWindow(c, $"Report Filter - {reportDef.Name}", null, 470).ShowDialog();

            // Update column defs
            return (dlres.HasValue && dlres.Value);
        }


    }
}
