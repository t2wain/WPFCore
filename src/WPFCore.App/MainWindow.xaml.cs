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
        #region Event

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // Status bar
            _svm = _sb.Init();

            // Main menu
            this._mainMenu.Init();

            InitElectIndex();
            InitElectGrid();

        }

        protected override void OnClosed(EventArgs e)
        {
            _tvm.PropertyChanged -= OnElectIndexPropertyChanged;
            _lvm.PropertyChanged -= OnElectGridPropertyChanged;
            base.OnClosed(e);
        }

        #endregion

        #region ElectIndex

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

        private void OnElectIndexPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_tvm.ItemCount):
                case nameof(_tvm.IsFocus):
                    if (_tvm.IsFocus)
                        UpdateCountMsg(this._tvm.ItemCount);
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

        #endregion

        #region ElectGrid

        LViewVM _lvm = null!;
        void InitElectGrid()
        {
            if (Application.Current is App app)
            {
                _lvm = app.Provider.GetRequiredService<LViewVM>();
                _lvm.PropertyChanged += OnElectGridPropertyChanged;
                this._lvw.Init(_lvm);
            }
        }

        private void OnElectGridPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_lvm.IsFocus):
                    if (_lvm.IsFocus)
                        UpdateCountMsg(this._lvm.ListData?.Count ?? 0);
                    break;
            }
        }

        #endregion

        #region Action

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


        private StatusBarVM _svm = null!;
        private void UpdateCountMsg(int count)
        {
            _svm.LeftMessage = count > 0 ? String.Format("Items: {0:#,###}", count) : "";
        }

        #endregion
    }
}