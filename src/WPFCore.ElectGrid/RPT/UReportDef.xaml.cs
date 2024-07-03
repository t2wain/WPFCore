﻿using System.Windows.Controls;
using System.Windows;
using WPFCore.Shared.UI.DG;
using R = WPFCore.Data.Report;

namespace WPFCore.ElectGrid.RPT
{
    /// <summary>
    /// Interaction logic for UReportDef.xaml
    /// </summary>
    public partial class UReportDef : UserControl
    {
        public UReportDef()
        {
            InitializeComponent();
        }

        public void Init(UReportDefVM vm)
        {
            this.DataContext = vm;
            SetupReportColumnDG(_dgCol, vm.ReportDef!.Columns!);
        }

        protected void SetupReportColumnDG(DataGrid dg, IEnumerable<R.ColumnDefinition> coldefs)
        {
            dg.AutoGenerateColumns = false;
            dg.ItemsSource = coldefs;
            dg.HorizontalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.VerticalGridLinesBrush = SystemColors.ControlLightBrush;
            dg.CanUserAddRows = false;
            dg.CanUserDeleteRows = false;

            var cols = new List<DataGridColumn>() 
            {
                DataGridUtility.CreateTextColumn("FieldName", "Field Name", 150),
                DataGridUtility.CreateTextColumn("HeaderName", "Header Name", 150, false),
                DataGridUtility.CreateTextColumn("Description", "Description", 200, false),
                DataGridUtility.CreateTextColumn("ColumnWidth", "Width", 50, false, HorizontalAlignment.Center),
                DataGridUtility.CreateCheckBoxColumn("IsPrimaryKey", "Key", 50, false),
                DataGridUtility.CreateCheckBoxColumn("Visible", "Visible", 50, false),
                DataGridUtility.CreateTextColumn("Position", "Position", 60, false, HorizontalAlignment.Center),
                DataGridUtility.CreateTextColumn("Format", "Format", 60, false, HorizontalAlignment.Center),
                DataGridUtility.CreateTextColumn("Alignment", "Alignment", 70, false, HorizontalAlignment.Center),
                DataGridUtility.CreateCheckBoxColumn("IsEditable", "Editable", 60, false),
                DataGridUtility.CreateCheckBoxColumn("IsLookUp", "Lookup", 60, false),
                DataGridUtility.CreateCheckBoxColumn("IsFrozen", "Frozen", 60, false),
                DataGridUtility.CreateTextColumn("Filter", "Filter", 150, false),
            };

            foreach (var col in cols)
                dg.Columns.Add(col);
        }

    }
   
}