using System.Windows.Controls;
using System.Windows;
using WPFCore.Shared.UI.LV;
using WPFCore.Data.Report;

namespace WPFCore.ElectGrid.LV
{
    /// <summary>
    /// Create GridView for a specific object collection
    /// or for a general report based on DataView
    /// </summary>
    public static class GridConfig
    {
        #region Specific report

        static GridView? _bom;
        public static GridView BOMItemsReport
        {
            get
            {
                if (_bom == null)
                    _bom = CreateBOMItemsReport();
                return _bom;
            }
        }

        #region BOMItemsReport

        static IDictionary<string, string> CreateBOMReportMapping()
        {
            var dm = new Dictionary<string, string>();

            dm.Add("BOM_No", "BOM_No");
            dm.Add("Material_Reference", "Material_Reference");
            dm.Add("Size_Code", "Size_Code");
            dm.Add("Total_MTO_Quantity", "Total_MTO_Quantity");
            dm.Add("Total_Issued_Qty", "Total_Issued_Qty");
            dm.Add("UOM", "UOM");
            dm.Add("BOM_Breakdown", "BOM_Breakdown");
            dm.Add("Material_Description", "Material_Description");
            dm.Add("Size_Description", "Size_Description");
            dm.Add("BOM_Fast_Access", "BOM_Fast_Access");
            dm.Add("Material_Fast_Access", "Material_Fast_Access");
            dm.Add("Requestor", "Requestor");
            dm.Add("Request_No", "Request_No");

            return dm;
        }


        static GridView CreateBOMItemsReport()
        {
            var m = CreateBOMReportMapping();

            var gv = new GridView();
            gv.Columns.Add(GridUtility.CreateColumn("BOM Document", m["BOM_No"], 160));
            gv.Columns.Add(GridUtility.CreateColumn("Package", m["Requestor"], 100));
            gv.Columns.Add(GridUtility.CreateColumn("Material Code", m["Material_Reference"], 150));
            gv.Columns.Add(GridUtility.CreateColumn("Size Code", m["Size_Code"], 80));
            gv.Columns.Add(GridUtility.CreateColumn("UOM", m["UOM"], 60));
            gv.Columns.Add(GridUtility.CreateColumn("MTO Qty", m["Total_MTO_Quantity"], 80, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("IPMS Ticket", m["Request_No"], 90));
            gv.Columns.Add(GridUtility.CreateColumn("Issued Qty", m["Total_Issued_Qty"], 80, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Ready", "Ready_Indic", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Allocated", "Allocated_Qty_WorkPlan", 80, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Work Plan", "Workplan", 120));
            gv.Columns.Add(GridUtility.CreateColumn("Work Plan Source", "Source_Doc_WorkPlan", 120));
            gv.Columns.Add(GridUtility.CreateColumn("Work Plan Avail.", "Source_Type_WorkPlan", 120, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Breakdown", m["BOM_Breakdown"], 120));
            gv.Columns.Add(GridUtility.CreateColumn("Material Desc", m["Material_Description"], 150));
            gv.Columns.Add(GridUtility.CreateColumn("Size Desc", m["Size_Description"], 120));
            gv.Columns.Add(GridUtility.CreateColumn("Sheet", "Sheet_No", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Rev", "Rev", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("BOM ID", m["BOM_Fast_Access"], 100));
            gv.Columns.Add(GridUtility.CreateColumn("Material ID", m["Material_Fast_Access"], 100));

            return gv;
        }

        #endregion

        #region TicketItemsReport

        static GridView CreateTicketItemsReport()
        {
            var gv = new GridView();
            gv.Columns.Add(GridUtility.CreateColumn("IPMS Ticket", "Request_No", 90));
            gv.Columns.Add(GridUtility.CreateColumn("Package", "Requestor", 100));
            gv.Columns.Add(GridUtility.CreateColumn("Material Code", "Material_Reference", 150));
            gv.Columns.Add(GridUtility.CreateColumn("Size Code", "Size_Code", 80));
            gv.Columns.Add(GridUtility.CreateColumn("MTO Qty", "MTO_Quantity", 80, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Issued Qty", "Issue_Qty", 80, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("UOM", "UOM", 60));
            gv.Columns.Add(GridUtility.CreateColumn("Ready", "Ready_Indic", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("BOM Doc", "BOM_No", 160));
            gv.Columns.Add(GridUtility.CreateColumn("Sheet", "Sheet_No", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Rev", "Rev", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("BOM ID", "BOM_Fast_Access", 100));
            gv.Columns.Add(GridUtility.CreateColumn("Material ID", "Material_Fast_Access", 100));

            return gv;
        }

        static GridView CreateProgressItemsReport()
        {
            var gv = new GridView();
            gv.Columns.Add(GridUtility.CreateColumn("Cobra Drawing", "drawing", 160));
            gv.Columns.Add(GridUtility.CreateColumn("Rev", "drawing_rev", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Package", "d13", 100));
            gv.Columns.Add(GridUtility.CreateColumn("Scan", "scan", 80));
            gv.Columns.Add(GridUtility.CreateColumn("Subscan", "subscan", 80));
            gv.Columns.Add(GridUtility.CreateColumn("Component", "component", 160));
            gv.Columns.Add(GridUtility.CreateColumn("Description", "description", 200));
            gv.Columns.Add(GridUtility.CreateColumn("Total Qty", "total_qty", 100, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("UOM", "uom", 60));
            gv.Columns.Add(GridUtility.CreateColumn("Rate", "rate", 80, typeof(double), HorizontalAlignment.Right, "{0:F4}"));
            gv.Columns.Add(GridUtility.CreateColumn("Total Hours", "total_hours", 100, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Pct Earn", "tot_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S1 Pct Earn", "s1_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S2 Pct Earn", "s2_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S3 Pct Earn", "s3_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S4 Pct Earn", "s4_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S5 Pct Earn", "s5_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S6 Pct Earn", "s6_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("S7 Pct Earn", "s7_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));
            gv.Columns.Add(GridUtility.CreateColumn("Unit", "unit", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("Area", "area", 60, typeof(String), HorizontalAlignment.Center));
            gv.Columns.Add(GridUtility.CreateColumn("ID", "record_id", 100));

            return gv;
        }

        #endregion

        #region PackageReport

        static GridView CreatePackageReport()
        {
            var gv = new GridView();
            gv.Columns.Add(GridUtility.CreateColumn("Package", "d13", 100));
            gv.Columns.Add(GridUtility.CreateColumn("Plan Hours", "tot_hrs", 100, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Earn Hours", "tot_fin_hrs", 100, typeof(double), HorizontalAlignment.Right, "{0:F1}"));
            gv.Columns.Add(GridUtility.CreateColumn("Pct Earn", "tot_fin_pct", 100, typeof(double), HorizontalAlignment.Right, "{0:F2}"));

            return gv;
        }

        #endregion

        #endregion

        #region General Report

        public static GridView CreateGeneralReport(ReportDefinition rdef)
        {
            var gv = new GridView();
            foreach (var c in rdef.Columns!)
            {
                gv.Columns.Add(GridUtility.CreateColumn(
                    c.HeaderName!, 
                    c.FieldName!, 
                    c.ColumnWidth,
                    Type.GetType(c.DataType!)!,
                    (HorizontalAlignment)c.Alignment,
                    c.Format!
                ));
            }
            return gv;
        }

        #endregion
    }
}
