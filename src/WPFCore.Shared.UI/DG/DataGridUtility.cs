using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using RPT = WPFCore.Data.Report;

namespace WPFCore.Shared.UI.DG
{
    public class DataGridUtility
    {

        #region Columns

        public static IDictionary<string, DataGridColumn> GetDictColumns(IEnumerable<DataGridColumn> dgCols) =>
            dgCols.ToDictionary(c => c switch
            {
                DataGridTextColumn tc => ((Binding)tc.Binding).Path.Path,
                DataGridComboBoxColumn cc => ((Binding)cc.SelectedItemBinding).Path.Path,
                _ => throw new NotImplementedException()
            });

        public static void UpdateColumnDefWidth(IEnumerable<RPT.ColumnDefinition> coldefs, IEnumerable<DataGridColumn> dgCols)
        {
            var dc = GetDictColumns(dgCols);
            foreach ( var coldef in coldefs )
            {
                if (dc.TryGetValue(coldef.FieldName!, out var gcol))
                    coldef.ColumnWidth = Convert.ToInt32(gcol.ActualWidth);
            }
        }

        public static DataGridTextColumn CreateTextColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true,
            HorizontalAlignment alignment = HorizontalAlignment.Left, 
            string? format = null, bool markEditable = true)
        {
            var c = new DataGridTextColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, alignment, format, markEditable);
            c.ElementStyle = CreateCellStyle(alignment);
            return c;
        }

        public static DataGridCheckBoxColumn CreateCheckBoxColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true, bool markEditable = true)
        {
            var c = new DataGridCheckBoxColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, HorizontalAlignment.Center, null, markEditable);
            return c;
        }

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

        internal static Style CreateCellStyle(HorizontalAlignment alignment)
        {
            Style style = new Style();
            style.TargetType = typeof(TextBlock);
            style.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, alignment));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top));
            style.Setters.Add(new Setter(TextBlock.PaddingProperty, new Thickness(5, 2, 5, 2)));
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

    }
}
