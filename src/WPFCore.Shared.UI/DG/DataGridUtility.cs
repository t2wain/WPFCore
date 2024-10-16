﻿using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using RPT = WPFCore.Data.Report;

namespace WPFCore.Shared.UI.DG
{
    public static class DataGridUtility
    {

        #region Columns

        public static IDictionary<string, DataGridColumn> GetDictColumns(IEnumerable<DataGridColumn> dgCols) =>
            dgCols.ToDictionary(c => c switch
            {
                DataGridTextColumn tc => ((Binding)tc.Binding).Path.Path,
                DataGridComboBoxColumn cc => ((Binding)cc.SelectedItemBinding).Path.Path,
                _ => throw new NotImplementedException()
            });

        /// <summary>
        /// Update the column width of ReportDefinition with the 
        /// actual width of the DataGrid columns
        /// </summary>
        public static void UpdateColumnDefWidth(IEnumerable<RPT.ColumnDefinition> coldefs, IEnumerable<DataGridColumn> dgCols)
        {
            var dc = GetDictColumns(dgCols);
            foreach ( var coldef in coldefs )
            {
                if (dc.TryGetValue(coldef.FieldName!, out var gcol))
                    coldef.ColumnWidth = Convert.ToInt32(gcol.ActualWidth);
            }
        }

        /// <summary>
        /// Create data-bound text column
        /// </summary>
        public static DataGridTextColumn CreateTextColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true,
            HorizontalAlignment alignment = HorizontalAlignment.Left, 
            string? format = null, bool markEditable = true)
        {
            var c = new DataGridTextColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, alignment, format, markEditable);
            c.ElementStyle = CreateTextCellStyle(alignment);
            return c;
        }

        /// <summary>
        /// Create data-bound checkbox column
        /// </summary>
        public static DataGridCheckBoxColumn CreateCheckBoxColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true, bool markEditable = true)
        {
            var c = new DataGridCheckBoxColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, HorizontalAlignment.Center, null, markEditable);
            c.ElementStyle = CreateCheckboxCellStyle();
            return c;
        }

        /// <summary>
        /// Create data-bound combobox column
        /// </summary>
        public static DataGridComboBoxColumn CreateComboBoxColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true,
            HorizontalAlignment alignment = HorizontalAlignment.Left, 
            string? format = null)
        {
            var c = new DataGridComboBoxColumn();
            c.SelectedValueBinding = CreateBinding(fieldName, format);
            c.Header = headerName;
            c.SortMemberPath = fieldName;
            c.IsReadOnly = isReadOnly;
            c.Width = width;

            //c.ElementStyle = CreateCellStyle(alignment);
            c.HeaderStyle = CreateHeaderStyle(alignment, isReadOnly);
            return c;
        }

        #endregion

        #region Common

        internal static void SetCommon(DataGridBoundColumn column, string fieldName,
            string headerName, bool isReadOnly, int width, HorizontalAlignment alignment, 
            string? format = null, bool markEditable = true)
        {
            column.Binding = CreateBinding(fieldName, format);
            column.Header = headerName;
            column.SortMemberPath = fieldName;
            column.IsReadOnly = isReadOnly;
            column.Width = width;

            column.HeaderStyle = CreateHeaderStyle(alignment, isReadOnly, markEditable);
        }

        internal static Binding CreateBinding(string bindingPath, string? format = null)
        {
            var b = new Binding(bindingPath);
            b.Mode = BindingMode.Default;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            if (!String.IsNullOrWhiteSpace(format))
                b.StringFormat = format;
            return b;
        }

        internal static Style CreateTextCellStyle(HorizontalAlignment alignment)
        {
            Style style = new Style();
            style.TargetType = typeof(TextBlock);
            style.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, alignment));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top));
            style.Setters.Add(new Setter(TextBlock.PaddingProperty, new Thickness(5, 2, 5, 2)));
            return style;
        }

        internal static Style CreateCheckboxCellStyle()
        {
            Style style = new Style();
            style.TargetType = typeof(CheckBox);
            style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center));
            return style;
        }

        internal static Style CreateHeaderStyle(HorizontalAlignment alignment, bool isReadOnly, bool markEditable = true)
        {
            Style style = new Style();
            style.TargetType = typeof(DataGridColumnHeader);
            style.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, alignment));
            if (!isReadOnly && markEditable)
            {
                style.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.Bold));
                style.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Blue));
            }
            return style;
        }


        #endregion

        #region DataGridCell copy and paste

        // Clean-up DataGridCell data before
        // copied into the Windows clipboard
        internal static (bool InValid, string? NewVal) ParseCellData(string? data, bool isCsv = false)
        {
            var val = data;
            bool invalid = false;
            if (!string.IsNullOrWhiteSpace(val) 
                && Regex.IsMatch(val, "[\u0022,\t\r\n]"))
            {
                val = val
                    .Replace("\u0022", "\u0022\u0022")
                    .Replace("\t", "\\t")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r");
                if (isCsv && Regex.IsMatch(val, "[\u0022,]"))
                    val = $"\u0022{val}\u0022";
                invalid = true;
            }
            return (invalid, val);
        }

        // Clean-up DataGridCell data before
        // copied into the Windows clipboard
        internal static (bool Invalid, IEnumerable<DataGridClipboardCellContent> Contents) ParseCellContent(
            IEnumerable<DataGridClipboardCellContent> contents)
        {
            var invalid = contents.Select(c => ParseCellData(c.Content?.ToString())).Any(c => c.InValid);
            if (invalid)
            {
                var q = contents.Select(c => new DataGridClipboardCellContent(c.Item, c.Column, 
                        ParseCellData(c.Content?.ToString()).NewVal)).ToList();
                return (true,  q);
            }
            else return (false, contents);
        }

        public delegate void UpdateDataGridCell(object item, DataGridColumn column, object data);

        /// <summary>
        /// Update the selected cells in the DataGrid
        /// with tab-delimited data
        /// </summary>
        /// <param name="v">row-column data as tab-delimited 
        /// and new lines typically from Windows clipborad</param>
        /// <param name="dgrid">DataGrid with selected cells</param>
        /// <returns>Success</returns>
        public static bool PasteIntoRowsAndColumns(string v, DataGrid dgrid, UpdateDataGridCell? updateAction = null)
        {
            var updatefn = updateAction ?? UpdateDataGridCellImpl;

            #region Split data

                // split data into rows
                // default DataGrid copy command contain extra new line ending
            var d = Regex.Replace(v, "\r\n$", ""); 
            string[] drows = d.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            // spit data into columns
            // when copy cell content via row-selection mode,
            // each text line is prefix with a tab.
            string[][] data = new string[drows.Length][];
            int ridx = 0;
            foreach (string drow in drows)
            {
                data[ridx++] = drow.Split('\t');
            }

            int drowCount = data.Length;
            int dcolCount = data[0].Length;

            #endregion

            // get row and column indexes of selected cells
            var cells = dgrid.SelectedCells;
            var lstCell = new List<(DataGridCellInfo Cell, DataGridRow Row, int RowIndex, int ColIndex)>();
            foreach (var cell in cells)
            {
                // Get the DataGridRow container
                var dp = dgrid.ItemContainerGenerator.ContainerFromItem(cell.Item);
                if (dp == null)
                {
                    // The rows in DataGrid is virtualized and so
                    // must be in view to obtain the DataGridRow container
                    dgrid.ScrollIntoView(cell.Item);
                    dp = dgrid.ItemContainerGenerator.ContainerFromItem(cell.Item);
                    if (dp == null) return false; // Still can't obtain the DataGridRow containter
                }

                var dgrow = (DataGridRow)dp;
                // get row and column indexes of selected cells
                lstCell.Add((cell, dgrow, dgrow.GetIndex(), cell.Column.DisplayIndex));
            }

            // calculate the top-left cell row and column index
            int initCellRowIdx = lstCell.Min(c => c.RowIndex);
            int initCellColIdx = lstCell.Min(c => c.ColIndex);

            foreach (var c in lstCell)
            {
                // calculate row and column index of data that
                // matches to the DataGridCell
                int drowIdx = (c.RowIndex - initCellRowIdx) % drowCount;
                int dcolIdx = c.ColIndex - initCellColIdx;
                if (!c.Cell.Column.IsReadOnly && drowIdx < drowCount && dcolIdx < dcolCount)
                    // Update the bounding DataRowView
                    updatefn(c.Cell.Item, c.Cell.Column, data[drowIdx][dcolIdx]);
            }

            return true;
        }

        static void UpdateDataGridCellImpl (object boundItem, DataGridColumn column, object data)
        {
            if (boundItem is DataRowView dr)
            {
                if (column is DataGridTextColumn col && col.Binding is Binding b)
                {
                    var fn = b.Path.Path;
                    dr[fn] = data;
                }
            }
        }

        #endregion
    }
}
