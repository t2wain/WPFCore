using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LV
{
    public class ListViewBinder
    {
        private ListView? _lvw;
        public ListView? ListViewControl
        {
            set
            {
                this._lvw = value;
                if (this._lvw != null && !DesignerProperties.GetIsInDesignMode(this._lvw))
                    this.InitListView(_lvw);
            }
            protected get
            {
                return this._lvw;
            }
        }

        protected ListViewVM? VM { get; set; }

        virtual protected void InitListView(ListView lv)
        {
            // Configure handlers for ListView events
            lv.AddHandler(ListView.MouseDownEvent, new RoutedEventHandler(this.OnMouseDown));
            lv.AddHandler(ListView.MouseRightButtonDownEvent, new RoutedEventHandler(this.OnItemMouseRightButtonDown));
            lv.AddHandler(ListView.ContextMenuOpeningEvent, new RoutedEventHandler(this.OnContextMenuOpen));
            lv.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(this.OnColumnHeaderClick));

            // Configure handlers for TreeViewItem events
            lv.AddHandler(ListViewItem.UnselectedEvent, new RoutedEventHandler(this.OnItemUnSelected));
            lv.AddHandler(ListViewItem.SelectedEvent, new RoutedEventHandler(this.OnItemSelected));

            this.ConfigCommands();

            if (lv.DataContext is ListViewVM vm)
            {
                this.VM = vm;
                this.VM.PropertyChanged += this.ListenPropertyChangedOnVM;
            }
        }

        #region ListView control event handler

        virtual protected void OnItemUnSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemSelected(object sender, RoutedEventArgs e) { }

        virtual protected void OnMouseDown(object sender, RoutedEventArgs e) { }

        virtual protected void OnItemMouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is DependencyObject dp)
            {
                var ti = Utility.FindParent<ListViewItem>(dp);
                if (ti != null)
                    ti.IsSelected = true;
            }
        }

        virtual protected void OnContextMenuOpen(object sender, RoutedEventArgs e)
        {
            var ti = this.ListViewControl?.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
            {
                e.Handled = !this.VM.IsContextMenuAllow(ti);
            }
        }

        virtual protected void OnColumnHeaderClick(object sender, RoutedEventArgs e) 
        {
            if (e.OriginalSource is GridViewColumnHeader col)
            {
                // allow column sortings
                bool isMultiColumn = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
                this.SortColumn(col.Name, isMultiColumn);
            }
        }

        #endregion

        #region Config Commands

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            this.VM?.RefreshData();
        }

        virtual protected void OnRefreshCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.RefreshMsg);
        }

        // Configure the handler for the commands
        virtual protected void ConfigCommands()
        {
            foreach (var cb in this.GetCommandBindings())
                this.ListViewControl?.CommandBindings.Add(cb);
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.Refresh, this.OnRefresh, this.OnRefreshCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allowed = false;
            var ti = this.ListViewControl?.SelectedItem as INotifyPropertyChanged;
            if (ti != null && this.VM != null)
                allowed = this.VM.IsCommandCanExecute(cmdName, ti);
            return allowed;
        }

        #endregion

        #region GridView

        virtual protected GridView CreateGridViewForDataView(DataView dv)
        {
            var gv = new GridView();
            DataTable t = dv.Table!;
            foreach (DataColumn dcol in t.Columns)
            {
                var col = this.CreateColumn(dcol.ColumnName, dcol.ColumnName, dcol.DataType);
                gv.Columns.Add(col);
            }
            return gv;
        }

        virtual protected GridViewColumn CreateColumn(string colName, string bindingPath, Type dataType)
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

            return this.CreateColumn(colName, bindingPath, width, dataType, alignment, format);
        }

        virtual protected GridViewColumn CreateColumn(string colName, string bindingPath, double width)
        {
            return this.CreateColumn(colName, bindingPath, width, typeof(String), HorizontalAlignment.Left, "");
        }

        virtual protected GridViewColumn CreateColumn(string colName, string bindingPath,
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
            col.CellTemplate = this.CreateDataTemplate(dataType, tb);


            return col;
        }

        virtual protected DataTemplate CreateDataTemplate(Type type, FrameworkElementFactory el)
        {
            DataTemplate template = new DataTemplate { DataType = type };
            template.VisualTree = el;
            return template;
        }

        virtual protected Style CreateViewStyle()
        {
            Style style = new Style();
            style.TargetType = typeof(ListViewItem);
            style.Setters.Add(new Setter(ListViewItem.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            return style;
        }

        #endregion

        virtual protected void SortColumn(string propertyName, bool isMultiColumn = false) 
        {
            this.VM?.SortColumn(propertyName, isMultiColumn);
        }

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }
    }
}
