using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using WPFCore.ElectGrid.LV;
using WPFCore.ElectIndex.LB;
using WPFCore.ElectIndex.TV;
using WPFCore.Shared.UI.SB;
using NT = WPFCore.ElectIndex.TV.TIndexNodeEnum;

namespace WPFCore.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }

        private StatusBarVM _svm = null!;
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // Status bar
            _svm = _sb.Init();

            // Main menu
            this._mainMenu.Init();

            InitElectIndex();
            InitElectGrid();
        }

        private UTreeLBoxVM _tvm = null!;
        void InitElectIndex()
        {
            if (Application.Current is App app)
            {
                _tvm = app.Provider.GetRequiredService<UTreeLBoxVM>();
                _tvm.PropertyChanged += OnElectIndexPropertyChanged;
                this._tvw.Init(_tvm);
            }
        }

        LViewVM _lvm = null!;
        void InitElectGrid()
        {
            if (Application.Current is App app)
            {
                _lvm = app.Provider.GetRequiredService<LViewVM>();
                this._lvw.Init(_lvm);
            }
        }

        private void OnElectIndexPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_tvm.ItemCount):
                    _svm.LeftMessage = _tvm.ItemCount > 0 ? $"Item count: {_tvm.ItemCount}" : "";
                    break;
                case UTreeLBoxVM.ExecuteViewDetailCmdTVEvent:
                    if (_tvm.TreeVM.SelectedItem is NodeVM t)
                        ShowDetail(t.DataItem);
                    break;
                case UTreeLBoxVM.ExecuteViewDetailCmdLBEvent:
                    if (_tvm.LBoxVM.SelectedItems.FirstOrDefault() is LBoxItemVM l)
                        ShowDetail(l.Data);
                    break;
            }
        }

        private void ShowDetail(TNodeData? item)
        {
            switch (item?.NodeType)
            {
                case NT.Report:
                    _lvm.ShowReport(item?.Data?.ID)
                        .ContinueWith(t => Task.CompletedTask);
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _tvm.PropertyChanged -= OnElectIndexPropertyChanged;
            this._tvw.Dispose();
            base.OnClosed(e);
        }
    }
}