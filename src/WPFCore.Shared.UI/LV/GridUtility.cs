using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFCore.Shared.UI.LV
{
    public static class GridUtility
    {
        public static GridView CreateGridViewForDataView(DataView dv)
        {
            var gv = new GridView();
            DataTable t = dv.Table!;
            foreach (DataColumn dcol in t.Columns)
            {
                var col = CreateColumn(dcol.ColumnName, dcol.ColumnName, dcol.DataType);
                gv.Columns.Add(col);
            }
            return gv;
        }

        public static GridViewColumn CreateColumn(string colName, string bindingPath, Type dataType)
        {
            string format = "";
            HorizontalAlignment alignment = HorizontalAlignment.Left;
            double width = 120;
            switch (dataType.Name)
            {
                case "Boolean":
                    format = "{0:Y;N}";
                    alignment = HorizontalAlignment.Center;
                    width = 60;
                    break;
                case "DateTime":
                    format = "dd-MMM-yyyy";
                    alignment = HorizontalAlignment.Center;
                    break;
                case "Decimal":
                case "Double":
                case "Single":
                    format = "#.#";
                    alignment = HorizontalAlignment.Right;
                    width = 60;
                    break;
                case "Int32":
                case "Int64":
                case "Int16":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    alignment = HorizontalAlignment.Right;
                    width = 60;
                    break;
            }

            //col.CellTemplate = this.CreateDataTemplate(dataType, tb);

            return CreateColumn(colName, bindingPath, width, dataType, alignment, format);
        }

        public static GridViewColumn CreateColumn(string colName, string bindingPath, double width)
        {
            return CreateColumn(colName, bindingPath, width, typeof(String), HorizontalAlignment.Left, "");
        }

        public static GridViewColumn CreateColumn(string colName, string bindingPath,
            double width, Type dataType, HorizontalAlignment alignment = HorizontalAlignment.Left, string format = "")
        {
            var b = new Binding(bindingPath);
            b.Mode = BindingMode.Default;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            if (!String.IsNullOrWhiteSpace(format))
                b.StringFormat = format;

            FrameworkElementFactory tb = new FrameworkElementFactory(typeof(TextBlock));
            tb.SetBinding(TextBlock.TextProperty, b);
            tb.SetValue(TextBlock.HorizontalAlignmentProperty, alignment);

            var col = new GridViewColumn();
            var header = new GridViewColumnHeader();
            header.Name = bindingPath;
            header.Content = colName;
            col.Header = header;
            col.Width = width;
            col.CellTemplate = CreateDataTemplate(dataType, tb);


            return col;
        }

        public static DataTemplate CreateDataTemplate(Type type, FrameworkElementFactory el)
        {
            DataTemplate template = new DataTemplate { DataType = type };
            template.VisualTree = el;
            return template;
        }

        public static Style CreateViewStyle()
        {
            Style style = new Style();
            style.TargetType = typeof(ListViewItem);
            style.Setters.Add(new Setter(ListViewItem.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            return style;
        }
    }
}
