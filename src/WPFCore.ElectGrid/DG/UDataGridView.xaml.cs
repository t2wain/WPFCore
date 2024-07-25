using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows.Controls;
using WPFCore.Common.Data;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.TC;

namespace WPFCore.ElectGrid.DG
{
    /// <summary>
    /// Interaction logic for UDataGridView.xaml
    /// </summary>
    public partial class UDataGridView : UserControl, IDisposable, IReportTabItemContent
    {
        public UDataGridView()
        {
            InitializeComponent();
        }

        bool _isInit = false;
        private DGBinder _dgvBind = null!;
        private DGridVM _vm = null!;

        public void Init(DGridVM vm)
        {
            if (!_isInit)
            {
                _vm = vm;
                this.DataContext = vm;
                this._dgvBind = new DGBinder();
                this._dgvBind.InitDataGrid(_dgData, vm);
                _isInit = true;
            }
        }

        internal void ShowEditWindow()
        {
            _dgvBind.ShowEditWindow();
        }

        internal void ShowFilterWindow()
        {
            _dgvBind.ShowFilterWindow();
        }

        internal void ClearFilter()
        {
            _dgvBind.ClearFilter();
        }

        public void Dispose()
        {
            this._dgvBind?.Dispose();
            this._dgvBind = null!;
        }

        #region IReportTabItemContent

        Task IReportTabItemContent.ShowReport(ReportDefinition rdef, IServiceProvider provider)
        {
            var dgvm = provider.GetRequiredService<DGridVM>();
            this.Init(dgvm);
            return _vm.ShowReport(rdef);
        }

        void IReportTabItemContent.ShowEditWindow() => this.ShowEditWindow();

        Task IReportTabItemContent.RefreshData() =>  this._vm.RefreshData();

        void IReportTabItemContent.SetFilter() => this.ShowFilterWindow();

        void IReportTabItemContent.ClearFilter() => this.ClearFilter();

        int IReportTabItemContent.ItemCount => this._vm.ItemCount;

        #endregion
    }
}
