using System.Windows;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.Shared.UI.DG;

namespace WPFCore.ElectGrid.DG
{
    public class DataGridConfig
    {
        #region General Report

        public static void SetDataGridOption(DataGrid dg, ReportDefinition rdef)
        {
            dg.AutoGenerateColumns = false;
            dg.HorizontalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.VerticalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.CanUserAddRows = rdef.AllowAddAndDelete;
            dg.CanUserDeleteRows = rdef.AllowAddAndDelete;
            dg.CanUserResizeRows = false;
            SetFrozenColumn(dg, rdef);
        }

        public static void SetFrozenColumn(DataGrid dg, ReportDefinition rdef) =>
            dg.FrozenColumnCount = rdef.Columns!.Where(c => c.IsFrozen).Count();

        public static IEnumerable<DataGridColumn> CreateGeneralReport(ReportDefinition rdef)
        {
            var cols = new List<DataGridColumn>();
            var q = rdef.Columns!
                .Where(i => i.Visible)
                .OrderByDescending(i => i.IsFrozen)
                .ThenBy(i => i.Position);

            foreach (var c in q)
            {
                if (c.IsLookUp && c.IsEditable)
                {
                    cols.Add(DataGridUtility.CreateComboBoxColumn(
                        c.FieldName!,
                        c.HeaderName!,
                        c.ColumnWidth,
                        !c.IsEditable,
                        (HorizontalAlignment)c.Alignment,
                        c.Format!
                    ));
                }
                else
                {
                    cols.Add(DataGridUtility.CreateTextColumn(
                        c.FieldName!,
                        c.HeaderName!,
                        c.ColumnWidth,
                        !c.IsEditable,
                        (HorizontalAlignment)c.Alignment,
                        c.Format!
                    ));

                }
            }
            return cols;
        }

        #endregion

    }
}
