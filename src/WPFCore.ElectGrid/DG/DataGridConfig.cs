using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.DG;
using RDF = WPFCore.Data.Report;

namespace WPFCore.ElectGrid.DG
{
    public class DataGridConfig
    {
        #region General Report

        public static void SetDataGridOption(DataGrid dg, RDF.ReportDefinition rdef)
        {
            dg.AutoGenerateColumns = false;
            dg.HorizontalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.VerticalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.CanUserAddRows = rdef.AllowAddAndDelete && !string.IsNullOrWhiteSpace(rdef.AddDbProcedure);
            dg.CanUserDeleteRows = rdef.AllowAddAndDelete && !string.IsNullOrWhiteSpace(rdef.DeleteDbProcedure);
            dg.CanUserResizeRows = false;
            dg.SelectionUnit = DataGridSelectionUnit.CellOrRowHeader;
            SetFrozenColumn(dg, rdef);
        }

        public static void SetFrozenColumn(DataGrid dg, RDF.ReportDefinition rdef) =>
            dg.FrozenColumnCount = rdef.Columns!.Where(c => c.IsFrozen).Count();

        public static IEnumerable<DataGridColumn> CreateGeneralReport(RDF.ReportDefinition rdef)
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
                        !IsColumnEditable(c, rdef),
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
                        !IsColumnEditable(c, rdef),
                        (HorizontalAlignment)c.Alignment,
                        c.Format!
                    ));

                }
            }
            return cols;
        }

        static bool IsColumnEditable(RDF.ColumnDefinition col, RDF.ReportDefinition rdef)
        {
            return col.IsEditable;
        }

        #endregion

    }
}
