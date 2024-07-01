using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFCore.Shared.UI.DG
{
    public class DataGridUtility
    {

        #region Columns

        public static DataGridTextColumn CreateTextColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true, 
            HorizontalAlignment alignment = HorizontalAlignment.Left, string? format = null)
        {
            var c = new DataGridTextColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, alignment, format);
            return c;
        }

        public static DataGridCheckBoxColumn CreateCheckBoxColumn(string fieldName,
            string headerName, int width, bool isReadOnly = true)
        {
            var c = new DataGridCheckBoxColumn();
            SetCommon(c, fieldName, headerName, isReadOnly, width, HorizontalAlignment.Center);
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

            c.ElementStyle = CreateCellStyle(alignment);
            c.HeaderStyle = CreateHeaderStyle(alignment, isReadOnly);
            return c;
        }

        #endregion

        #region Common

        internal static void SetCommon(DataGridBoundColumn column, string fieldName,
            string headerName, bool isReadOnly, int width, HorizontalAlignment alignment, string? format = null)
        {
            column.Binding = CreateBinding(fieldName, format);
            column.Header = headerName;
            column.SortMemberPath = fieldName;
            column.IsReadOnly = isReadOnly;
            column.Width = width;

            column.ElementStyle = CreateCellStyle(alignment);
            column.HeaderStyle = CreateHeaderStyle(alignment, isReadOnly);
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

        internal static Style CreateHeaderStyle(HorizontalAlignment alignment, bool isReadOnly)
        {
            Style style = new Style();
            style.TargetType = typeof(DataGridColumnHeader);
            style.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, alignment));
            if (!isReadOnly)
                style.Setters.Add(new Setter(TextBlock.BackgroundProperty, Brushes.Aqua));
            return style;
        }


        #endregion

    }
}
