using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.DG;
using WPFCore.ElectGrid.TC;

namespace WPFCore.ElectGrid.LV
{
    /// <summary>
    /// Interaction logic for UListView.xaml
    /// </summary>
    public partial class UListView : UserControl, IDisposable, IReportTabItemContent
    {
        public UListView()
        {
            InitializeComponent();
        }

        bool _isInit = false;
        private LViewBinder _lvwBind = null!;
        LViewVM _vm = null!;

        public void Init(LViewVM vm)
        {
            if (!_isInit)
            {
                _vm = vm;
                this.DataContext = vm;
                this._lvwBind = new LViewBinder();
                this._lvwBind.InitListView(_lvwData, vm);
                _isInit = true;
            }
        }

        public void Dispose()
        {
            this._lvwBind?.Dispose();
            this._lvwBind = null!;
        }

        #region IReportTabItemContent

        Task IReportTabItemContent.ShowReport(ReportDefinition rdef, IServiceProvider provider)
        {
            var lvm = provider.GetRequiredService<LViewVM>();
            this.Init(lvm);
            return _vm.ShowReport(rdef);
        }

        void IReportTabItemContent.ShowEditWindow() =>
            throw new NotImplementedException();

        Task IReportTabItemContent.RefreshData() => this._vm.RefreshData();
 
        void IReportTabItemContent.SetFilter() =>
            throw new NotImplementedException();

        void IReportTabItemContent.ClearFilter() =>
            throw new NotImplementedException();

        int IReportTabItemContent.ItemCount => this._vm.ItemCount;

        #endregion
    }
}
